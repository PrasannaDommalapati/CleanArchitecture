using Domain.BaseEntities;
using Domain.Entities;
using FluentAssertions;
using NetArchTest.Rules;

namespace Domain.Tests;

public class ArchitectureTests
{
    private readonly Types _entities = Types.InNamespace(typeof(Applicant).Namespace);

    [Fact]
    public void EntitiesShouldBeSealed()
    {
        var result = _entities.Should().BeSealed().GetResult().IsSuccessful;

        result.Should().BeTrue();
    }

    [Fact]
    public void EntitiesShouldDependOnBaseAuditableEntity()
    {
        var baseAuditableEntityFullName = typeof(BaseAuditableEntity)?.FullName;
        var result = _entities.Should().HaveDependencyOn(baseAuditableEntityFullName).GetResult().IsSuccessful;

        result.Should().BeTrue();
    }
}