using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DesignPatternDemo.Behavioral.Mediator
{
    class ChatRoom
    {
        public delegate void Callback(string message, string from);
        // The Mediator is in two parts: Interact handles the GUI;
        // Mediator itself sets up the threads and does the
        // broadcast from the delegate (Send method)
        class Mediator
        {
            Callback Respond;
            public void SignOn(string name, Callback Receive, Interact visuals)
            {
                // Add the Colleague to the delegate chain
                Respond += Receive;
                new Thread((ParameterizedThreadStart)delegate(object o)
                {
                    visuals.InputEvent += Send;
                    Application.Run(visuals);
                }).Start(this);
                // Wait to load the GUI
                while (visuals == null || !visuals.IsHandleCreated)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
            // Send implemented as a broadcast
            public void Send(string message, string from)
            {
                // Message not sent if it contains "work"
                if (message.IndexOf("work") == -1)
                    if (Respond != null)
                        Respond(message, from);
            }
        }

        class Interact : Form
        {
            TextBox wall;
            Button sendButton;
            TextBox messageBox;
            string name;
            public Interact(string name)
            {
                // GUI construction not shown but includes
                // sendButton.Click += new EventHandler(Input);
            }
            public event Callback InputEvent;
            public void Input(object source, EventArgs e)
            {
                if (InputEvent != null)
                    InputEvent(messageBox.Text, name);
            }
            public void Output(string message)
            {
                if (this.InvokeRequired)
                    this.Invoke((MethodInvoker)delegate() { Output(message); });
                else
                {
                    wall.AppendText(message + "\r\n");
                    messageBox.Clear();
                    this.Show();
                }
            }
        }
        class Colleague
        {
            Interact visuals;
            string name;
            public Colleague(Mediator mediator, string name)
            {
                this.name = name;
                visuals = new Interact(name);
                mediator.SignOn(name, Receive, visuals);
            }
            public void Receive(string message, string from)
            {
                visuals.Output(from + ": " + message);
            }
        }

        static void Demo()
        {
            Mediator m = new Mediator();
            Colleague chat1 = new Colleague(m, "John");
            Colleague chat2 = new Colleague(m, "David");
            Colleague chat3 = new Colleague(m, "Lucy");
        }
    }
}
