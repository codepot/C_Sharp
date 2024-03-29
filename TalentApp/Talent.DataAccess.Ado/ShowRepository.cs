﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Talent.Domain;
using Ucla.Common.ExtensionMethods;
using Ucla.Common.Interfaces;

namespace Talent.DataAccess.Ado
{
    public class ShowRepository : IRepository<Show>
    {
        #region IRepository interface

        public IEnumerable<Show> Fetch(object criteria = null)
        {
            var data = new List<Show>();
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    if (criteria == null)
                    {
                        var sql = new StringBuilder();
                        sql.Append("select * from Show; ");
                        sql.Append("select * from ShowGenre; ");
                        sql.Append("select * from Credit; ");
                        cmd.CommandText = sql.ToString();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var s = new Show();
                            s.ShowId = dr.AsInt32("ShowId");
                            s.Title = dr.AsString("Title");
                            s.LengthInMinutes = dr.AsNullableInt32("LengthInMinutes");
                            s.MpaaRatingId = dr.AsNullableInt32("MpaaRatingId");
                            s.TheatricalReleaseDate
                                = dr.AsNullableDateTime("TheatricalReleaseDate");
                            s.DvdReleaseDate = dr.AsNullableDateTime("DvdReleaseDate");

                            s.IsDirty = false;
                            data.Add(s);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var g = new ShowGenre();
                            g.ShowGenreId = dr.AsInt32("ShowGenreId");
                            g.ShowId = dr.AsInt32("ShowId");
                            g.GenreId = dr.AsInt32("GenreId");

                            g.IsDirty = false;
                            data.Where(o => o.ShowId == g.ShowId).Single().ShowGenres.Add(g);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var c = new Credit();
                            c.CreditId = dr.AsInt32("CreditId");
                            c.ShowId = dr.AsInt32("ShowId");
                            c.PersonId = dr.AsInt32("PersonId");
                            c.CreditTypeId = dr.AsInt32("CreditTypeId");
                            c.Character = dr.AsString("Character");

                            c.IsDirty = false;
                            data.Where(o => o.ShowId == c.ShowId).Single().Credits.Add(c);
                        }
                    }
                    else if (criteria is ShowCriteria)
                    {
                        var sql = new StringBuilder();
                        sql.Append(
                            @"select * from Show 
                                where Title like '%' + @Title + '%' 
                                and (@MpaaRatingId = 0
                                or MpaaRatingId = @MpaaRatingId); 
                                ");
                        sql.Append(
                            @"select sg.* from ShowGenre sg 
                                join Show s on sg.ShowId = s.ShowId 
                                where s.Title like '%' + @Title + '%' 
                                and (@MpaaRatingId = 0
                                or s.MpaaRatingId = @MpaaRatingId);
                                ");
                        sql.Append(
                            @"select c.* from Credit c 
                                join Show s on c.ShowId = s.ShowId 
                                where s.Title like '%' + @Title + '%' 
                                and (@MpaaRatingId = 0 
                                or s.MpaaRatingId = @MpaaRatingId); 
                                ");
                        cmd.CommandText = sql.ToString();
                        cmd.Parameters.AddWithValue("@Title", ((ShowCriteria)criteria).Title);
                        cmd.Parameters.AddWithValue("@MpaaRatingId", ((ShowCriteria)criteria)
                            .MpaaRatingId);
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var s = new Show();
                            s.ShowId = dr.AsInt32("ShowId");
                            s.Title = dr.AsString("Title");
                            s.LengthInMinutes = dr.AsNullableInt32("LengthInMinutes");
                            s.MpaaRatingId = dr.AsNullableInt32("MpaaRatingId");
                            s.TheatricalReleaseDate
                                = dr.AsNullableDateTime("TheatricalReleaseDate");
                            s.DvdReleaseDate = dr.AsNullableDateTime("DvdReleaseDate");
                            s.IsDirty = false;
                            data.Add(s);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var g = new ShowGenre();
                            g.ShowGenreId = dr.AsInt32("ShowGenreId");
                            g.ShowId = dr.AsInt32("ShowId");
                            g.GenreId = dr.AsInt32("GenreId");
                            g.IsDirty = false;
                            data.Where(o => o.ShowId == g.ShowId).Single().ShowGenres.Add(g);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var c = new Credit();
                            c.CreditId = dr.AsInt32("CreditId");
                            c.ShowId = dr.AsInt32("ShowId");
                            c.PersonId = dr.AsInt32("PersonId");
                            c.CreditTypeId = dr.AsInt32("CreditTypeId");
                            c.Character = dr.AsString("Character");
                            c.IsDirty = false;
                            data.Where(o => o.ShowId == c.ShowId).Single().Credits.Add(c);
                        }
                    }
                    else if (criteria is int)
                    {
                        var sql = new StringBuilder();
                        sql.Append("select * from Show where ShowId = @ShowId; \r\n");
                        sql.Append("select * from ShowGenre where ShowId = @ShowId; \r\n");
                        sql.Append("select * from Credit where ShowId = @ShowId; ");
                        cmd.CommandText = sql.ToString();
                        cmd.Parameters.AddWithValue("@ShowId", (int)criteria);
                        var dr = cmd.ExecuteReader();
                        var s = new Show();
                        while (dr.Read())
                        {
                            s.ShowId = dr.AsInt32("ShowId");
                            s.Title = dr.AsString("Title");
                            s.LengthInMinutes = dr.AsNullableInt32("LengthInMinutes");
                            s.MpaaRatingId = dr.AsNullableInt32("MpaaRatingId");
                            s.TheatricalReleaseDate
                                = dr.AsNullableDateTime("TheatricalReleaseDate");
                            s.DvdReleaseDate = dr.AsNullableDateTime("DvdReleaseDate");

                            s.IsDirty = false;
                            data.Add(s);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var g = new ShowGenre();
                            g.ShowGenreId = dr.AsInt32("ShowGenreId");
                            g.ShowId = dr.AsInt32("ShowId");
                            g.GenreId = dr.AsInt32("GenreId");

                            g.IsDirty = false;
                            s.ShowGenres.Add(g);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var c = new Credit();
                            c.CreditId = dr.AsInt32("CreditId");
                            c.ShowId = dr.AsInt32("ShowId");
                            c.PersonId = dr.AsInt32("PersonId");
                            c.CreditTypeId = dr.AsInt32("CreditTypeId");
                            c.Character = dr.AsString("Character");

                            s.IsDirty = false;
                            s.Credits.Add(c);
                        }
                    }
                    else
                    {
                        var msg = String.Format(
                            "ShowRepository: Unknown criteria type: {0}",
                            criteria);
                        throw new InvalidOperationException(msg);
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// Saves entity changes to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated entity, or null if the entity is deleted</returns>
        public Show Persist(Show item)
        {
            if (item.ShowId == 0 && item.IsMarkedForDeletion)
            {
                item = null;
            }

            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;
            using (TransactionScope ts = new TransactionScope())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    if (item.IsMarkedForDeletion)
                    {
                        // Also Deletes Children
                        DeleteEntity(item, conn);
                        item = null;
                    }
                    else if (item.ShowId == 0)
                    {
                        if(item.Error != null)
                        {
                            throw new ApplicationException(
                                "The Show data is invalid and cannot be saved");
                        }
                        InsertEntity(item, conn);
                        PersistChildren(item, conn);
                        item.IsDirty = false;
                    }
                    else if (item.IsDirty)
                    {
                        if (item.Error != null)
                        {
                            throw new ApplicationException(
                                "The Show data is invalid and cannot be saved");
                        }
                        UpdateEntity(item, conn);
                        PersistChildren(item, conn);
                        item.IsDirty = false;
                    }
                    else
                    {
                        // No changes to show, but might be changes to children
                        if (item.Error != null)
                        {
                            throw new ApplicationException(
                                "The Show data is invalid and cannot be saved");
                        }
                        PersistChildren(item, conn);
                    }
                }
                ts.Complete();
            }
            return item;
        }

        private static void PersistChildren(Show show, SqlConnection conn)
        {
            foreach (var showGenre in show.ShowGenres)
            {
                showGenre.ShowId = show.ShowId;
                ShowGenreHelper.Persist(showGenre, conn);
            }
            foreach (var credit in show.Credits)
            {
                credit.ShowId = show.ShowId;
                CreditHelper.Persist(credit, conn);
            }
        }


        #endregion

        #region SQL methods

        internal static void DeleteEntity(Show item, SqlConnection conn)
        {
            // Cascade delete ShowGenres
            foreach (var genre in item.ShowGenres)
            {
                ShowGenreHelper.DeleteEntity(genre, conn);
            }

            // Cascade delete Credits
            foreach (var credit in item.Credits)
            {
                CreditHelper.DeleteEntity(credit, conn);
            }

            // Delete Show itself
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete Show where ShowId = @ShowId";
                cmd.Parameters.AddWithValue("@ShowId", item.ShowId);
                cmd.ExecuteNonQuery();
            }
        }

        internal static void InsertEntity(Show item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("insert Show (Title, LengthInMinutes, "
                    + "TheatricalReleaseDate, DvdReleaseDate, MpaaRatingId)");
                sql.Append("values ( @Title, @LengthInMinutes, "
                    + " @TheatricalReleaseDate, @DvdReleaseDate, @MpaaRatingId);");
                sql.Append("select cast ( scope_identity() as int);");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);
                item.ShowId = (int)cmd.ExecuteScalar();
            }
        }

        internal static void UpdateEntity(Show item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("update Show set ");
                sql.Append(" Title = @Title, ");
                sql.Append(" LengthInMinutes = @LengthInMinutes, ");
                sql.Append(" TheatricalReleaseDate = @TheatricalReleaseDate, ");
                sql.Append(" DvdReleaseDate = @DvdReleaseDate, ");
                sql.Append(" MpaaRatingId = @MpaaRatingId ");
                sql.Append("where ShowId = @ShowId");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);
                cmd.Parameters.AddWithValue("@ShowId", item.ShowId);

                cmd.ExecuteNonQuery();
            }
        }

        private static void SetCommonParameters(Show item, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Title", item.Title);
            cmd.Parameters.AddWithValue("@LengthInMinutes",
                item.LengthInMinutes.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@TheatricalReleaseDate",
                item.TheatricalReleaseDate.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@DvdReleaseDate",
                item.DvdReleaseDate.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@MpaaRatingId",
                item.MpaaRatingId.AsSqlParameterValue());
        }


        #endregion

    }
}

