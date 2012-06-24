
$(function ()
{
    $('#openFileDialog').click(getFilesToUpload);
});

function getFilesToUpload()
{
    alert("adfasdfsd");
    var dialog = new ActiveXObject('MSComDlg.CommonDialog');
    dialog.Filter = 'All Files(*.*)|*.*';
    dialog.MaxFileSize = 32767;
    dialog.DialogTitle = 'Select Files to Upload';
    dialog.Flags = 0x200 | 0x80000 | 0x800 | 0x4 | 0x200000
    dialog.ShowOpen();
    //some downstream process here
}