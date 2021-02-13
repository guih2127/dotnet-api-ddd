using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }

        [Fact(DisplayName="Crud Usuario")]
        [Trait("CRUD", "Usuario")]
        public async Task PossivelRealizarCRUDUsuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity entity = new UserEntity()
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var usuarioCriado = await _repository.InsertAsync(entity);
                Assert.NotNull(usuarioCriado);
                Assert.Equal(entity.Email, usuarioCriado.Email);
                Assert.Equal(entity.Name, usuarioCriado.Name);
                Assert.False(usuarioCriado.Id == Guid.Empty);

                entity.Name = Faker.Name.First();
                var registroAtualizado = await _repository.UpdateAsync(entity);
                Assert.NotNull(registroAtualizado);
                Assert.Equal(entity.Email, registroAtualizado.Email);
                Assert.Equal(entity.Name, registroAtualizado.Name);

                var registroExiste = await _repository.ExistsAsync(entity.Id);
                Assert.True(registroExiste);

                var registroSelecionado = await _repository.SelectAsync(entity.Id);
                Assert.True(registroExiste);
                Assert.Equal(entity.Email, registroSelecionado.Email);
                Assert.Equal(entity.Name, registroSelecionado.Name);

                var todosRegistros = await _repository.GetAllAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                var removeu = await _repository.DeleteAync(entity.Id);
                Assert.True(removeu);
            }
        }
    }
}