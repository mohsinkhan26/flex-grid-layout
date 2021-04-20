using UnityEngine;

namespace MK.FlexGridLayout.Example
{
    public class TestFlexGrid : MonoBehaviour
    {
        [SerializeField] private FlexGridLayout flexGridLayout;

        void Start()
        {
            // just for testing
            Add8TestEntry();
            Add8TestEntry();
            AddTestEntry();
            AddTestEntry();
        }

        [ContextMenu("Add Test Entry")]
        void AddTestEntry()
        {
            if (flexGridLayout != null)
                flexGridLayout.AddEntry(Random.Range(1, 99999999).ToString(), false, AddTestEntry,
                    (_textToRemove) => { });
        }

        [ContextMenu("Add 8 Test Entry")]
        void Add8TestEntry()
        {
            if (flexGridLayout != null)
            {
                for (int i = 0; i < 8; ++i)
                    flexGridLayout.AddEntry(Random.Range(1, 99999999).ToString(), false, AddTestEntry,
                        (_textToRemove) => { });
            }
        }
    }
}
