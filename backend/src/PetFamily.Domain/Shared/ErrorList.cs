using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors;


    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = errors.ToList();
    }


    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
