using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector3Int gridPosition, int playerID, CurrencyManager currencyManager);
    void UpdateState(Vector3Int gridPosition);
}