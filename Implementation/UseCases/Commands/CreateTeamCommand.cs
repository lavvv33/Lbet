using Application.DTO;
using Application.UseCases;
using Application.UseCases.Commands;
using DataAccess;
using Domain;

namespace Implementation.UseCases.Commands;

public class CreateTeamCommand : ICreateTeamCommand
{
    private readonly LBetContext _context;

    public CreateTeamCommand(LBetContext context)
    {
        _context = context;
    }

    public int Id => 1;
    public string Name => "New Team Creation";

    public void Execute(CreateTeamDTO req)
    {
        var team = new Team
        {
            Name = req.Name
        };

        _context.Teams.Add(team);
        _context.SaveChanges();
    }
}