using Application.Commands.Applicant;
using FluentAssertions;
using MediatR;
using NetArchTest.Rules;

namespace Application.Tests.Commands.Applicant;
public class ApplicantAddCommadHandlerTests
{
    [Fact]
    public void ApplicantAddRequest_should_inherit_from_IRequest()
    {
        var tt = Types.InNamespace(typeof(ApplicantAddRequest).Namespace).That().HaveName(nameof(ApplicantAddRequest));
        var result = tt
            .Should()
            .Inherit(typeof(IRequest)).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void ApplicantAddResponse_should_inherit_from_IResponse()
    {

    }
}
