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
    public class ProductoDA
    {
        public Producto LlenarEntidad(IDataReader reader)
        {
            Producto producto = new Producto();

            if (reader["IdProducto"] != DBNull.Value)
                producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);

            if (reader["IdCategoria"] != DBNull.Value)
                producto.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);

            if (reader["DescCategoria"] != DBNull.Value)
                producto.DescCategoria = Convert.ToString(reader["DescCategoria"]);

            if (reader["NomProducto"] != DBNull.Value)
                producto.NomProducto = Convert.ToString(reader["NomProducto"]);

            if (reader["MarcaProducto"] != DBNull.Value)
                producto.MarcaProducto = Convert.ToString(reader["MarcaProducto"]);

            if (reader["ModeloProducto"] != DBNull.Value)
                producto.ModeloProducto = Convert.ToString(reader["ModeloProducto"]);

            if (reader["LineaProducto"] != DBNull.Value)
                producto.LineaProducto = Convert.ToString(reader["LineaProducto"]);

            if (reader["GarantiaProducto"] != DBNull.Value)
                producto.GarantiaProducto = Convert.ToString(reader["GarantiaProducto"]);

            if (reader["Precio"] != DBNull.Value)
                producto.Precio = Convert.ToDecimal(reader["Precio"]);

            /*if (reader["Imagen"] != DBNull.Value)
                producto.Imagen = Convert.ToString(reader["Imagen"]);*/

            if (reader["DescripcionTecnica"] != DBNull.Value)
                producto.DescripcionTecnica = Convert.ToString(reader["DescripcionTecnica"]);          


            return producto;
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> ListaEntidad = new List<Producto>();
            Producto entidad = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paListarProductos", conexion))
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
        public Producto InsertarProducto(Producto producto)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paProducto_insertar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                    comando.Parameters.AddWithValue("@NomProducto", producto.NomProducto);
                    comando.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto);
                    comando.Parameters.AddWithValue("@ModeloProducto", producto.ModeloProducto);
                    comando.Parameters.AddWithValue("@LineaProducto", producto.LineaProducto);
                    comando.Parameters.AddWithValue("@GarantiaProducto", producto.GarantiaProducto);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@DescripcionTecnica", producto.DescripcionTecnica);

                    conexion.Open();
                    producto.IdProducto = Convert.ToInt32(comando.ExecuteScalar());
                    conexion.Close();
                }
            }
            return producto;
        }
        public Producto ModificarProducto(int IdProducto, Producto producto)
        {
            Producto SegSSOMProducto = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {

                using (SqlCommand comando = new SqlCommand("paProducto_Modificar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    comando.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                    comando.Parameters.AddWithValue("@NomProducto", producto.NomProducto);
                    comando.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto);
                    comando.Parameters.AddWithValue("@ModeloProducto", producto.ModeloProducto);
                    comando.Parameters.AddWithValue("@LineaProducto", producto.LineaProducto);
                    comando.Parameters.AddWithValue("@GarantiaProducto", producto.GarantiaProducto);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@DescripcionTecnica", producto.DescripcionTecnica);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        SegSSOMProducto = LlenarEntidad(reader);

                    }

                    conexion.Close();
                }
            }
            return SegSSOMProducto;
        }
        public bool Eliminar(int idProducto)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("paProducto_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
