using ArteViva.Data;
using Microsoft.EntityFrameworkCore;
/////////////////////////////////////////////////////////////////////
// Criar o banco de dados e as tabelas com dados iniciais

using var db = new AppDbContext();
db.Database.Migrate();   // cria o banco "arteviva", as tabelas e os dados iniciais

Console.WriteLine("Banco criado com sucesso!");
//////////////////////////////////////////////////////////////////////