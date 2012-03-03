using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//分解复杂判断
//与尽快返回有些像
namespace LosTechies.DaysOfRefactoring.SampleCode.ArrowheadAntipattern.Before
{
    public class Permission
    { }

    public interface ISecurityChecker
    {
        bool CheckPermission(User user, Permission permission);

    }

    public class User
    { }

    public class Security
    {
        public ISecurityChecker SecurityChecker { get; set; }

        public Security(ISecurityChecker securityChecker)
        {
            SecurityChecker = securityChecker;
        }

        /// <summary>
        /// Bad smell.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        /// <param name="exemptions"></param>
        /// <returns></returns>
        public bool HasAccess(User user, Permission permission, IEnumerable<Permission> exemptions)
        {
            bool hasPermission = false;

            if (user != null)
            {
                if (permission != null)
                {
                    if (exemptions.Count() == 0)
                    {
                        if (SecurityChecker.CheckPermission(user, permission) || exemptions.Contains(permission))
                        {
                            hasPermission = true;
                        }
                    }
                }
            }

            return hasPermission;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.ArrowheadAntipattern.After
{
    public class Permission
    { }

    public interface ISecurityChecker
    {
        bool CheckPermission(User user, Permission permission);
    }

    public class User
    { }

    public class Security
    {
        public ISecurityChecker SecurityChecker { get; set; }

        public Security(ISecurityChecker securityChecker)
        {
            SecurityChecker = securityChecker;
        }

        public bool HasAccess(User user, Permission permission, IEnumerable<Permission> exemptions)
        {
            if (user == null || permission == null)
                return false;

            if (exemptions.Contains(permission))
                return true;

            return SecurityChecker.CheckPermission(user, permission);
        }
    }
}