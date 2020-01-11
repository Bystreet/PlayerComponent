﻿// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SQLite;
using UnityEngine.AI;

namespace wovencode
{

	// ===================================================================================
	// DatabaseManager
	// ===================================================================================
	public partial class DatabaseManager
	{
		
		// ============================= PRIVATE METHODS =================================
		
		// -------------------------------------------------------------------------------
		[DevExtMethods("Init")]
		void Init_Player()
		{
	   		CreateTable<TablePlayer>();
		}
		
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("CreateDefaultData")]
		void CreateDefaultData_Player(GameObject player)
		{
			/*
				players have no default data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadDataWithPriority")]
		void LoadDataWithPriority_Player(GameObject player)
		{
			/*
				players do not load priority data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("LoadData")]
		void LoadData_Player(GameObject player)
		{
	   		/*
				players do not load any data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("SaveData")]
		void SaveData_Player(GameObject player)
		{
	   		Execute("UPDATE TablePlayer SET lastsaved=? WHERE name=?", DateTime.UtcNow, player.name);
		}
		
		// -------------------------------------------------------------------------------
	   	// DeleteData_Player
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("DeleteData")]
	   	void DeleteData_Player(string _name)
	   	{
	   		Execute("DELETE FROM TablePlayer WHERE name=?", _name);
	   	}
		
		// ============================ PROTECTED METHODS ================================
		
		// -------------------------------------------------------------------------------
		// PlayerSetOnline
		// Sets the player online (1) or offline (0) and updates last login time
		// -------------------------------------------------------------------------------
		protected void PlayerSetOnline(string _name, int _action=1)
		{
			Execute("UPDATE TablePlayer SET online=?, lastlogin=? WHERE name=?", _action, DateTime.UtcNow, _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetDeleted
		// Sets the player to deleted (1) or undeletes it (0)
		// -------------------------------------------------------------------------------
		protected void PlayerSetDeleted(string _name, int _action=1)
		{
			Execute("UPDATE TablePlayer SET deleted=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetBanned
		// Bans (1) or unbans (0) the player
		// -------------------------------------------------------------------------------
		protected void PlayerSetBanned(string _name, int _action=1)
		{
			Execute("UPDATE TablePlayer SET banned=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		// EreasePlayer
		// Hard Delete = permanently deletes the player and all of its data
		// -------------------------------------------------------------------------------
		protected void EreasePlayer(string _name)
		{			
			this.InvokeInstanceDevExtMethods("DeleteData", _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetConfirmed
		// Sets the player to confirmed (1) or unconfirms it (0)
		// -------------------------------------------------------------------------------
		protected void PlayerSetConfirmed(string _name, int _action=1)
		{
			Execute("UPDATE TablePlayer SET confirmed=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		protected bool TryHardDelete(string _name, string _password)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!PlayerExists(_name))
					return false;

				EreasePlayer(_name);
				return true;	
				
			}
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		
	}

}