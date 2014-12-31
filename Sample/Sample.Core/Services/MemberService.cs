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
		IList<Member> Get();

		/// <summary>
		/// Gets a singe Member based on the given primary key
		/// </summary>
		Member Get(int id);
		
		/// <summary>
		/// Saves a Member
		/// </summary>
		void Save(Member member);
		
		/// <summary>
		/// Saves a list of Member
		/// </summary>
		void Save(IEnumerable<Member> members);
		
		/// <summary>
		/// Inserts a Member
		/// </summary>
		void Insert(Member member);

		/// <summary>
		/// Inserts a list of Member
		/// </summary>
		void Insert(IEnumerable<Member> members);
		
		/// <summary>
		/// Updates a Member
		/// </summary>
		void Update(Member member);

		/// <summary>
		/// Updates a list of Member
		/// </summary>
		void Update(IEnumerable<Member> members);
		
		/// <summary>
		/// Deletes a Member
		/// </summary>
		void Delete(Member member);

		/// <summary>
		/// Deletes a list of Member
		/// </summary>
		void Delete(IEnumerable<Member> members);
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
		public IList<Member> Get()
		{
			return _repository.Get();
		}

		/// <summary>
		/// Gets a singe Member based on the given primary key
		/// </summary>
		public Member Get(int id)
		{
			Member member = _repository.Get(id);
			
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
		/// Inserts a Member
		/// </summary>
		public void Insert(Member member)
		{
			_repository.Insert(member);
		}
		
		/// <summary>
		/// Inserts a list of Member
		/// </summary>
		public void Insert(IEnumerable<Member> members)
		{
			_repository.Insert(members);
		}
		
		/// <summary>
		/// Updates a Member
		/// </summary>
		public void Update(Member member)
		{
			_repository.Update(member);
		}
		
		/// <summary>
		/// Updates a list of Member
		/// </summary>
		public void Update(IEnumerable<Member> members)
		{
			_repository.Update(members);
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