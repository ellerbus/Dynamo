using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Repositories;
using Sample.Core.Models;

namespace Sample.Core.Services
{
	#region Service interface
	
	/// <summary>
	/// Service Interface for Member
	/// </summary>
	public interface IMemberService 
	{
		/// <summary>
		/// Gets a list of Members
		/// </summary>
		/// <returns></returns>
		IList<Member> GetList();

		/// <summary>
		/// Gets a singe Member based on the given primary key
		/// </summary>
		Member Get(int memberId);
		
		/// <summary>
		/// Saves a Member
		/// </summary>
		void Save(Member member);
		
		/// <summary>
		/// Saves a list of Member
		/// </summary>
		void Save(IEnumerable<Member> members);
		
		///// <summary>
		///// Deletes a Member
		///// </summary>
		//void Delete(Member member);
		//
		/// <summary>
		///// Deletes a list of Member
		///// </summary>
		//void Delete(IEnumerable<Member> members);
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
		/// Gets a list of Members
		/// </summary>
		/// <returns></returns>
		public IList<Member> GetList()
		{
			return _repository.GetList();
		}

		/// <summary>
		/// Gets a singe Member based on the given primary key
		/// </summary>
		public Member Get(int memberId)
		{
			Member member = _repository.Get(memberId);
			
			return member;
		}
		
		/// <summary>
		/// Saves a Member
		/// </summary>
		/// <returns></returns>
		public void Save(Member member)
		{
			_repository.Save(member);
		}
		
		/// <summary>
		/// Saves a list of Members
		/// </summary>
		/// <returns></returns>
		public void Save(IEnumerable<Member> members)
		{
			_repository.Save(members);
		}
		
		/// <summary>
		/// Deletes a Member
		/// </summary>
		public void Delete(Member member)
		{
			_repository.Delete(member);
		}
		
		/// <summary>
		/// Deletes a list of Member
		/// </summary>
		public void Delete(IEnumerable<Member> members)
		{
			_repository.Delete(members);
		}

		#endregion
	}
}