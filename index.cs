1
namespace DemoIdentity.Models
2
{
3
    public class ApplicationUser : IdentityUser
4
    {
5
    }
6
 
7
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
8
    {
9
        public ApplicationDbContext()
10
            : base("DefaultConnection")
11
        {
12
        }
13
    }
14
}
1
namespace DemoIdentity.Models
2
{
3
    public class ApplicationUser : IdentityUser
4
    {
5
        public string Nome { get; set; }
6
 
7
        public string Sobrenome { get; set; }
8
 
9
        public string Email { get; set; }
10
    }
11
 
12
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
13
    {
14
        public ApplicationDbContext()
15
            : base("DefaultConnection")
16
        {
17
        }
18
    }
19
}
public class RegisterViewModel
2
{
3
    [Required]
4
    [Display(Name = "User name")]
5
    public string UserName { get; set; }
6
 
7
    [Required]
8
    public string Nome { get; set; }
9
 
10
    [Required]
11
    public string Sobrenome { get; set; }
12
 
13
    [Required]
14
    [Display(Name = "E-mail")]
15
    [EmailAddress]
16
    public string Email { get; set; }
17
 
18
    [Required]
19
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
20
    [DataType(DataType.Password)]
21
    [Display(Name = "Password")]
22
    public string Password { get; set; }
23
 
24
    [DataType(DataType.Password)]
25
    [Display(Name = "Confirm password")]
26
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
27
    public string ConfirmPassword { get; set; }
28
}
..........................................................................................


private void btnListar_Click(object sender, EventArgs e)
02
{
03
    var dataTable = new DataTable();
04
 
05
    // Retorna para a variavel a ConnectionString configurada no App.Config
06
    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
07
 
08
    // Cria uma instancia de conexão com o banco de dados
09
    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
10
    {
11
        // Abre a conexão
12
        connection.Open();
13
 
14
        // Cria uma instancia do command
15
        using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())
16
        {
17
            // Comando SQL que será executado
18
            var _sqlQuery = "SELECT * FROM CLIENTES";
19
 
20
            command.Connection = connection;
21
            command.CommandText = _sqlQuery;
22
 
23
            // Adiciona o resultado em um DataTable
24
            using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command))
25
            {
26
                adapter.Fill(dataTable);
27
            }
28
        }
29
 
30
        // Fecha conexão
31
        connection.Close();
32
    }
33
 
34
    // Atribui o resultado ao grid
35
    gridClientes.DataSource = dataTable;
36
    // Gera automaticamente as colunas
37
    gridClientes.AutoGenerateColumns = true;
38
    // Muda o modo de seleção da grid para linha inteira
39
    gridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
40
}
private void btnExcluir_Click(object sender, EventArgs e)
02
{
03
    if (gridClientes.SelectedRows.Count > 0)
04
    {
05
        // Pega o ID da primeira coluna da linha selecionada e converte para Integer
06
        int id;
07
        int.TryParse(gridClientes.SelectedRows[0].Cells[0].Value.ToString(), out id);
08
 
09
        // Retorna para a variavel a ConnectionString configurada no App.Config
10
        var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
11
 
12
        // Cria uma instancia de conexão com o banco de dados
13
        using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
14
        {
15
            // Abre a conexão
16
            connection.Open();
17
 
18
            // Cria uma instancia do command
19
            using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())
20
            {
21
                // Comando SQL que será executado
22
                var _sqlQuery = "DELETE FROM CLIENTES WHERE ID = @ID";
23
 
24
                command.Connection = connection;
25
                command.CommandText = _sqlQuery;
26
                command.Parameters.AddWithValue("id", id);
27
 
28
                // Executa a query
29
                command.ExecuteNonQuery();
30
            }
31
 
32
            // Fecha conexão
33
            connection.Close();
34
        }
35
 
36
        // Invoca o botão listar
37
        btnListar_Click(sender, e);
38
    }
39
}
