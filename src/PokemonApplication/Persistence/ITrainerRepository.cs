using PokemonDomain.TrainerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApplication.Persistence;
public interface ITrainerRepository
{
    Task<bool> CpfExistsAsync(string cpf);
    Task<Trainer> CreateTrainerAsync(string name, int age, string cpf);
}
