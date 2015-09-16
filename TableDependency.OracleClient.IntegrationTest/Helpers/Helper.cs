﻿using System;
using Oracle.DataAccess.Client;

namespace TableDependency.OracleClient.IntegrationTest.Helpers
{
    internal static class Helper
    {
        internal static bool AreAllDbObjectDisposed(string connectionString, string dbObjectNaming)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT COUNT(*) FROM user_OBJECTS WHERE OBJECT_TYPE = 'TRIGGER' AND UPPER(OBJECT_NAME) = 'TR_{dbObjectNaming}'";
                    var triggerExistis = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT COUNT(*) FROM user_OBJECTS WHERE OBJECT_TYPE = 'PROCEDURE' AND UPPER(OBJECT_NAME) = 'DEQ_{dbObjectNaming}'";
                    var procedureExistis = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT COUNT(*) FROM user_OBJECTS WHERE OBJECT_TYPE = 'QUEUE' AND UPPER(OBJECT_NAME) = 'QUE_{dbObjectNaming}'";
                    var queueExistis = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT COUNT(*) FROM user_OBJECTS WHERE OBJECT_TYPE = 'TABLE' AND UPPER(OBJECT_NAME) = 'QT_{dbObjectNaming}'";
                    var tableQueueExistis = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT COUNT(*) FROM user_OBJECTS WHERE OBJECT_TYPE = 'TYPE' AND UPPER(OBJECT_NAME) = 'TYPE_{dbObjectNaming}'";
                    var typeExistis = Convert.ToInt32(command.ExecuteScalar());

                    return (triggerExistis == 0 && queueExistis == 0 && procedureExistis == 0 && tableQueueExistis == 0 && typeExistis == 0);
                }
            }
        }
    }
}