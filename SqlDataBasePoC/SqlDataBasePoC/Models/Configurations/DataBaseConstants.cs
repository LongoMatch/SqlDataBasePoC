using System;
namespace SqlDataBasePoC.Models.Configurations
{
	public class DataBaseConstants
	{
		// In data base enviroment is common to call tables and fields using the snake case nomenclature
		// i.e. customer_phone_number
		//
		public const string TABLE_TEAMS = "teams";
        public const string TABLE_TEAMS_FIELD_NAME = "name";
        public const string TABLE_TEAMS_FIELD_COUNTRY = "country";

        public const string TABLE_PLAYERS = "players";
        public const string TABLE_PLAYERS_FIELD_NAME = "name";
        public const string TABLE_PLAYERS_FIELD_POSITION = "position";
    }
}

