using CrudExamen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;


namespace CrudExamen.Controllers
{
    public class HomeController : Controller
    {

        //private static string cnStr = System.Configuration.ConfigurationManager.ConnectionStrings["cnStr"].ToString();
        
        
        private static List<ClienteCompras> lCompras = new List<ClienteCompras>();

        string cnStr = "Server=(local);Database=Compras;User Id=sa;Password=poison;TrustServerCertificate=True"; 


        private readonly ClienteContext _dbContext;
        public HomeController(ClienteContext dbcontext) { _dbContext = dbcontext; }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            //return View(_dbContext.Clientes.OrderBy(e=> e.Apellidos));

            //return View(_dbContext.Cliente_Compras.OrderBy(e => e.Apellidos));

            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("select * from Cliente_compras", Cn);
            SqlDataReader Dr;
            //Cmd.CommandType = System.Data.CommandType.TableDirect;
            Cn.Open();
            Dr = Cmd.ExecuteReader();
            lCompras.Clear();
            while (Dr.Read())
            {
                ClienteCompras oclientecompras = new ClienteCompras();
                oclientecompras.ClienteId = Convert.ToInt32(Dr["ClienteId"]);
                oclientecompras.Nombres = Dr["Nombres"].ToString();
                oclientecompras.Apellidos = Dr["Apellidos"].ToString();
                oclientecompras.TipoCliente = Dr["TipoCliente"].ToString();
                oclientecompras.SituacionLaboral = Dr["SituacionLaboral"].ToString();
                oclientecompras.Estado = Dr["Estado"].ToString();
                oclientecompras.NroCompras = Convert.ToInt32(Dr["NroCompras"]); 
                lCompras.Add(oclientecompras);
            }

            Cn.Close();
            return View(lCompras);

        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            _dbContext.Clientes.Add(cliente);
            _dbContext.SaveChanges();
            return RedirectToAction("Listar");
        }

        public IActionResult Eliminar(int id)
        {
            return View(_dbContext.Clientes.FirstOrDefault(e => e.ClienteId == id));
        }

        [HttpPost]
        public IActionResult Eliminar(Cliente cliente)
        {

            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("cliente_desactivar", Cn);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("clienteid", cliente.ClienteId);
            Cn.Open();
            Cmd.ExecuteNonQuery();
            Cn.Close();
            return RedirectToAction("Listar");
        }

        public IActionResult Editar(int id)
        {
            return View(_dbContext.Clientes.FirstOrDefault(e => e.ClienteId == id));
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            _dbContext.Clientes.Update(cliente);
            _dbContext.SaveChanges();
            return RedirectToAction("Listar");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}