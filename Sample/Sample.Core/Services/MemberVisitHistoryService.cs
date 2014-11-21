using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Repositories;
using Sample.Core.Models;

namespace Sample.Core.Services
{
	#region Service interface
	
	/// <summary>
	/// Service Interface for MemberVisitHistory
	/// </summary>
	public interface IMemberVisitHistoryService 
	{
		/// <summary>
		/// Gets a list of MemberVisitHistories
		/// </summary>
		/// <returns></returns>
		IList<MemberVisitHistory> GetList();

		/// <summary>
		/// Gets a singe MemberVisitHistory based on the given primary key
		/// </summary>
		MemberVisitHistory Get(int memberId, DateTime visitedAt);
		
		/// <summary>
		/// Saves a MemberVisitHistory
		/// </summary>
		void Save(MemberVisitHistory memberVisitHistory);
		
		/// <summary>
		/// Saves a list of MemberVisitHistory
		/// </summary>
		void Save(IEnumerable<MemberVisitHistory> memberVisitHistories);
		
		///// <summary>
		///// Deletes a MemberVisitHistory
		///// </summary>
		//void Delete(MemberVisitHistory memberVisitHistory);
		//
		/// <summary>
		///// Deletes a list of MemberVisitHistory
		///// </summary>
		//void Delete(IEnumerable<MemberVisitHistory> memberVisitHistories);
	}
	
	#endregion

	/// <summary>
	/// Service Implementation for MemberVisitHistory
	/// </summary>
	public class MemberVisitHistoryService : IMemberVisitHistoryService 
	{
		#region Members

		private IMemberVisitHistoryRepository _repository;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public MemberVisitHistoryService(IMemberVisitHistoryRepository repository)
		{
			_repository = repository;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a list of MemberVisitHistories
		/// </summary>
		/// <returns></returns>
		public IList<MemberVisitHistory> GetList()
		{
			//	TODO implement stored procedure to get
			//	a list of MemberVisitHistory
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a singe MemberVisitHistory based on the given primary key
		/// </summary>
		public MemberVisitHistory Get(int memberId, DateTime visitedAt)
		{
			MemberVisitHistory memberVisitHistory = _repository.Get(memberId, visitedAt);
			
			return memberVisitHistory;
		}
		
		/// <summary>
		/// Saves a MemberVisitHistory
		/// </summary>
		/// <returns></returns>
		public void Save(MemberVisitHistory memberVisitHistory)
		{
			_repository.Save(memberVisitHistory);
		}
		
		/// <summary>
		/// Saves a list of MemberVisitHistories
		/// </summary>
		/// <returns></returns>
		public void Save(IEnumerable<MemberVisitHistory> memberVisitHistories)
		{
			_repository.Save(memberVisitHistories);
		}
		
		/// <summary>
		/// Deletes a MemberVisitHistory
		/// </summary>
		public void Delete(MemberVisitHistory memberVisitHistory)
		{
			_repository.Delete(memberVisitHistory);
		}
		
		/// <summary>
		/// Deletes a list of MemberVisitHistory
		/// </summary>
		public void Delete(IEnumerable<MemberVisitHistory> memberVisitHistories)
		{
			_repository.Delete(memberVisitHistories);
		}

		#endregion
	}
}