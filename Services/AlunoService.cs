using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _dbContext;

        public AlunoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                var aluno = await _dbContext.Alunos.FindAsync(id);
                return aluno;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _dbContext.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<Aluno>> GetAlunoByNome(string nome)
        {
            try
            {
                IEnumerable<Aluno> aluno;
                if (!string.IsNullOrWhiteSpace(nome))
                {
                    aluno = await _dbContext.Alunos.Where(n => n.Nome == nome).ToListAsync();
                }
                else
                {
                    aluno = await GetAlunos();
                }
                return aluno;
            }
            catch
            {
                throw;
            }
        }


        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _dbContext.Alunos.Add(aluno);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _dbContext.Entry(aluno).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _dbContext.Alunos.Remove(aluno);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
