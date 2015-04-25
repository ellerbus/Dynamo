using System;
using System.Configuration;
using Augment;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;

namespace NerdBudget.Core.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Member
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool VerifyLogin(string name, string password);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Member
    /// </summary>
    public class MemberService : IMemberService
    {
        #region Members

        private IMemberRepository _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Verifies a user in the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyLogin(string name, string password)
        {
            name = name.AssertNotNull().ToLower();

            bool isvalid = false;

            Member member = _repository.Get(name);

            if (member == null)
            {
                if (name == ConfigurationManager.AppSettings["InitUser"])
                {
                    member = new Member()
                    {
                        Name = name,
                        Password = BCrypt.Net.BCrypt.HashPassword(password),
                        LoggedInAt = DateTime.UtcNow
                    };

                    Utilities.AuditUpdate(member);

                    _repository.Save(member);

                    isvalid = true;
                }
            }
            else
            {
                isvalid = BCrypt.Net.BCrypt.Verify(password, member.Password);
            }

            if (isvalid)
            {
                member.LoggedInAt = DateTime.UtcNow;

                Utilities.AuditUpdate(member);

                _repository.Save(member);
            }

            return isvalid;
        }

        #endregion
    }
}