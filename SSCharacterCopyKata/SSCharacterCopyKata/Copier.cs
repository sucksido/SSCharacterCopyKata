using System.Collections.Generic;

public class Copier
{
    private readonly ISource _inputSource;
    private readonly IDestination _outputDestination;

    public Copier(ISource inputSource, IDestination outputDestination)
    {
        _inputSource = inputSource;
        _outputDestination = outputDestination;
    }

    // Method for copying single characters until newline
    public void CopySingleCharacters()
    {
        char currentCharacter;
        while ((currentCharacter = _inputSource.RetrieveSingleCharacter()) != '\n')
        {
            _outputDestination.AcceptSingleCharacter(currentCharacter);
        }
    }

    // Bonus method for copying multiple characters until newline
    public void CopyMultipleCharacters(int characterCount)
    {
        char[] charactersBatch = _inputSource.RetrieveMultipleCharacters(characterCount);
        var charactersToWrite = new List<char>();

        foreach (var individualCharacter in charactersBatch)
        {
            if (individualCharacter == '\n') break;
            charactersToWrite.Add(individualCharacter);
        }

        _outputDestination.AcceptMultipleCharacters(charactersToWrite.ToArray());
    }
}