using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ReadNote.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
