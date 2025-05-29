using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class UsuariosDA
    {
        public Usuario LlenarEntidad(IDataReader reader)
        {
            Usuario usuario = new Usuario();

            if (reader["IdUsuario"] != DBNull.Value)
                usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

            if (reader["CodUsuario"] != DBNull.Value)
                usuario.CodUsuario = Convert.ToString(reader["CodUsuario"]);

            if (reader["Nombres"] != DBNull.Value)
                usuario.Nombres = Convert.ToString(reader["Nombres"]);

            if (reader["IdRol"] != DBNull.Value)
                usuario.IdRol = Convert.ToInt32(reader["IdRol"]);

            if (reader["DesRol"] != DBNull.Value)
                usuario.DesRol = Convert.ToString(reader["DesRol"]);

            return usuario;
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> ListaEntidad = new List<Usuario>();
            Usuario entidad = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("ListarUsuarios", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = LlenarEntidad(reader);
                        ListaEntidad.Add(entidad);
                    }
                }
                conexion.Close();
            }
            return ListaEntidad;
        }
        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                //  string UserPass = Utilitario.GetMd5Hash2(pPassword);
                byte[] UserPass = EncriptacionHelper.EncriptarByte(pPassword);
                int returnedVal = 0;
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
                {
                    using (SqlCommand comando = new SqlCommand("paUsuario_BuscaCodUserClave", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@ParamUsuario", pUsuario);
                        comando.Parameters.AddWithValue("@ParamPass", UserPass);
                        conexion.Open();
                        returnedVal = Convert.ToInt32(comando.ExecuteScalar());
                        conexion.Close();
                    }

                }

                return Convert.ToInt32(returnedVal);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return -1;
            }
        }
        public Usuario InsertarUsuario(Usuario usuario)
        {
            byte[] UserPass = EncriptacionHelper.EncriptarByte(usuario.ClaveTxt);
            usuario.Clave = UserPass;

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_insertar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    conexion.Open();
                    usuario.IdUsuario = Convert.ToInt32(comando.ExecuteScalar());
                    conexion.Close();
                }
            }
            return usuario;
        }
        public Usuario ModificarUsuario(int IdUsuario, Usuario usuario)
        {
            Usuario SegSSOMUsuario = null;
            byte[] UserPass = EncriptacionHelper.EncriptarByte(usuario.ClaveTxt);
            usuario.Clave = UserPass;

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {

                using (SqlCommand comando = new SqlCommand("paUsuario_Modificar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        SegSSOMUsuario = LlenarEntidad(reader);

                    }

                    conexion.Close();
                }
            }
            return SegSSOMUsuario;
        }




    }
}

