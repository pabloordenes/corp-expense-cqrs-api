using FluentAssertions;
using NetArchTest.Rules;

namespace CorpExpenseApi.UnitTests.Architecture;

public class LayerTests
{
    private const string ApplicationNamespace = "CorpExpenseApi.Application";
    private const string InfrastructureNamespace = "CorpExpenseApi.Infrastructure";
    private const string ApiNamespace = "CorpExpenseApi.Api";
    
    [Fact]
    public void Domain_Should_Not_HaveDependencyOn_OtherLayers()
    {
        // Arrange: Cargamos el ensamblado de Domain en memoria mediante reflexión
        var domainAssembly = typeof(Domain.Entities.Expense).Assembly;

        var otherLayers = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            ApiNamespace
        };

        // Act: Aplicamos la regla arquitectónica
        var result = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherLayers)
            .GetResult();

        // Assert: Validamos que la regla sea exitosa
        result.IsSuccessful.Should().BeTrue("El Dominio es el núcleo y no debe depender de ninguna otra capa.");
    }
}