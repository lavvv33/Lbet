namespace Application.UseCases;

public interface ICommand<TData> : IUseCase
{
    void Execute(TData data);
}