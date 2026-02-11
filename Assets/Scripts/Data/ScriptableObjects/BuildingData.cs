using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
    public class BuildingData : ScriptableObject
    {
        [field: SerializeField] public Material DefaultMaterial {get;  private set;}
        [field: SerializeField] public Material ShadesMaterial {get;  private set;}
        [field: SerializeField] public Material EditModMaterial {get;  private set;}
        
        [field: SerializeField] public Color AvailableColor {get;  private set;}
        [field: SerializeField] public Color UnavailableColor {get;  private set;}
        [field: SerializeField] public Color DefaultColor {get;  private set;}
    }
}