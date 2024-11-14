public interface IDestination
{
    void AcceptSingleCharacter(char characterToWrite);
    void AcceptMultipleCharacters(char[] charactersToWrite);
}