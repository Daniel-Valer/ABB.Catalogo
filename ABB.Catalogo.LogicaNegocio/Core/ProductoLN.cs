﻿using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class ProductoLN : BaseLN
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                ProductoDA productos = new ProductoDA();
                return productos.ListarProductos();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return lista;
            }
        }

    
    public Producto InsertarProducto(Producto producto)
        {
            try
            {
                return new ProductoDA().InsertarProducto(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;

            }
        }
    
    public Producto ModificarProducto(int IdProducto, Producto producto)
        {
            try
            {
                return new ProductoDA().ModificarProducto(IdProducto, producto);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;

            }
        }
        public Producto BuscaProductoId(int pProductoId)
        {
            try
            {
                ProductoDA producto = new ProductoDA();
                return producto.BuscaProductoId(pProductoId);


            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }
        public bool Eliminar(int idProducto)
        {
            try
            {
                ProductoDA producto = new ProductoDA();
                return producto.Eliminar(idProducto);


            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }
    }
}

