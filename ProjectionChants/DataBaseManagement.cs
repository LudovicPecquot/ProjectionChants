using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Collections.Generic;

namespace ProjectionChants
{
    class DataBaseManagement
    {
        private string CmdString = string.Empty;
        private String _dataFileName;

        public void setDataFileName(String dataFileName)
        {
            _dataFileName = dataFileName;
        }
        public String getDataFileName()
        {
            return _dataFileName;
        }

        private string ConString()
        {
            if (_dataFileName == null)
            {
                _dataFileName = Directory.GetCurrentDirectory() + "\\chants.sdf";
            }
            return "DataSource=" + _dataFileName;
        }

        public DataTable LoadSongs()
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "SELECT id, nom, theme, cle, numero, refrain, ref_size, tab, tab_size, nb_couplet FROM chants";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                SqlCeDataAdapter sda = new SqlCeDataAdapter(cmd);
                DataTable dt = new DataTable("chants");
                sda.Fill(dt);
                return dt;
            }
        }

        public DataTable LoadVerse(int song_id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "SELECT id, couplet_id, couplet, tab, tab_size FROM couplet WHERE chant_id=@chant_id";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@chant_id", song_id);
                SqlCeDataAdapter sda = new SqlCeDataAdapter(cmd);
                DataTable dt = new DataTable("couplets");
                sda.Fill(dt);
                return dt;
            }
        }

        public void InsertSong(string titre, string theme, string cle, string numero, string refrain, double ref_size, string tab, int tab_size, SortedList<int, Couplet> couplets)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "INSERT INTO chants (nom,theme,cle,numero,refrain,ref_size,tab,tab_size,nb_couplet) VALUES (@nom,@theme,@cle,@numero,@refrain,@ref_size,@tab,@tab_size,@nb_couplet)";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@nom", titre);
                cmd.Parameters.AddWithValue("@theme", theme);
                cmd.Parameters.AddWithValue("@cle", cle);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@refrain", refrain);
                cmd.Parameters.AddWithValue("@ref_size", ref_size);
                cmd.Parameters.AddWithValue("@tab", tab);
                cmd.Parameters.AddWithValue("@tab_size", tab_size);
                cmd.Parameters.AddWithValue("@nb_couplet", couplets.Count);
                con.Open();
                cmd.ExecuteNonQuery();
                SqlCeCommand lastId = new SqlCeCommand("SELECT @@IDENTITY", con);
                int songId = Convert.ToInt32(lastId.ExecuteScalar());
                con.Close();
                for (int i = 1; i <= couplets.Count; i++)
                {
                    InsertVerse(i, couplets[i].Text, couplets[i].Tab, couplets[i].TabSise, songId);
                }
            }
        }

        public void InsertVerse(int couplet_id, string couplet, string tab, int tab_size, int chant_id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "INSERT INTO couplet (chant_id,couplet_id,couplet,tab,tab_size) VALUES (@chant_id,@couplet_id,@couplet,@tab,@tab_size)";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@chant_id", chant_id);
                cmd.Parameters.AddWithValue("@couplet_id", couplet_id);
                cmd.Parameters.AddWithValue("@couplet", couplet);
                cmd.Parameters.AddWithValue("@tab", tab);
                cmd.Parameters.AddWithValue("@tab_size", tab_size);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateSong(string titre, string theme, string cle, string numero, string refrain, double ref_size, string tab, int tab_size, SortedList<int, Couplet> couplets, int id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "UPDATE chants SET nom=@nom,theme=@theme,cle=@cle,numero=@numero,refrain=@refrain,ref_size=@ref_size,tab=@tab,tab_size=@tab_size,nb_couplet=@nb_couplet WHERE id=@id";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nom", titre);
                cmd.Parameters.AddWithValue("@theme", theme);
                cmd.Parameters.AddWithValue("@cle", cle);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@refrain", refrain);
                cmd.Parameters.AddWithValue("@ref_size", ref_size);
                cmd.Parameters.AddWithValue("@tab", tab);
                cmd.Parameters.AddWithValue("@tab_size", tab_size);
                cmd.Parameters.AddWithValue("@nb_couplet", couplets.Count);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                for (int i = 1; i <= couplets.Count; i++)
                {
                    if (couplets[i].Id == 0)
                    {
                        InsertVerse(i, couplets[i].Text, couplets[i].Tab, couplets[i].TabSise, id);
                    }
                    else
                    {
                        UpdateVerse(i, couplets[i].Text, couplets[i].Tab, couplets[i].TabSise, id, couplets[i].Id);
                    }
                }
            }
        }

        public void UpdateVerse(int couplet_id, string couplet, string tab, int tab_size, int chant_id, int id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "UPDATE couplet SET couplet_id=@couplet_id,couplet=@couplet,tab=@tab,tab_size=@tab_size,chant_id=@chant_id WHERE id = @id";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@chant_id", chant_id);
                cmd.Parameters.AddWithValue("@couplet_id", couplet_id);
                cmd.Parameters.AddWithValue("@couplet", couplet);
                cmd.Parameters.AddWithValue("@tab", tab);
                cmd.Parameters.AddWithValue("@tab_size", tab_size);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteSong(int id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "DELETE FROM chants WHERE id = @id";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteVerse(int id)
        {
            using (SqlCeConnection con = new SqlCeConnection(ConString()))
            {
                CmdString = "DELETE FROM couplet WHERE id = @id";
                SqlCeCommand cmd = new SqlCeCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
