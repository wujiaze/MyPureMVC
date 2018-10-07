using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppByPureMVC;
using SimpleUIFramework;
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public class UserEmployeeInfo:MonoBehaviour
    {
        private UserForm _userForm;
        private UserList _userList;
        private static UserEmployeeInfo _instance;
        public static UserEmployeeInfo Instance {
            get { return _instance; }
        }

        public UserForm UserForm
        {
            get { return _userForm; }
        }

        public UserList UserList
        {
            get { return _userList; }
        }

        void Awake()
        {
            _instance = this;
            _userList = UnityHelper.AddTheChildNodeComopnetScripts<UserList>(gameObject, Proconst.EMP_LISTINFO);
            _userForm = UnityHelper.AddTheChildNodeComopnetScripts<UserForm>(gameObject, Proconst.EMP_FORMINFO);
        }
    }
}
