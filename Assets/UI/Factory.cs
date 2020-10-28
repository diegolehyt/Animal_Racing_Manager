using System.Collections;
using System.Collections.Generic;
using Unity.UIElements.Runtime;
using UnityEngine.UIElements;
using UnityEngine;

public class Factory : MonoBehaviour
{
    // Creating variable for panel render obj
    private PanelRenderer panelRend;
    
    // OnEnable function to get UXML file from the Panel in Unity
    private void OnEnable()
    {
        panelRend = GetComponent<PanelRenderer>();

        // Checking the Panel is not null to continue
        if (panelRend != null)
            panelRend.postUxmlReload += onLoadUxml; 
    }


    // ===++++++++++++++++++++++++++++++++++++++++\ APP Runs from here /+++++++++++++++++++++++++++++++++++++++++===

    // Function to get elements from nodes in UXML (DOM)
    private IEnumerable<Object> onLoadUxml()
    {
        // Verification of null element
        if (panelRend != null)
        {
            // Loads all element from Visualtree into root variable
            var root = panelRend.visualTree;

            var nombre = root.Q<Label>("nombre");
            var boton = root.Q<Button>("boton");

            var collectBtn1 = root.Q<Button>("collect1");

            if (collectBtn1 != null)
            {
                collectBtn1.clickable.clicked += CollectLine1;
            }

            if (boton != null)
            {
                boton.clickable.clicked += Clickable_clicke;
            }

            // TIMERS ("functionName", when to start, interval time)

            // Invoke("GoldUp", 2f);
            InvokeRepeating("GoldUp", 1f, 1f);

            InvokeRepeating("ProductionLineUp1", 1f, 1f);
        }
        return null;
    }

    // -----------------------------------  OnClick event handlers  --------------------------------------
    private void Clickable_clicke()
    {
        var root = panelRend.visualTree;
        var boton = root.Q<Button>("boton");

        // Production Line 1 event, to add Line 2
        var productionLine2 = root.Q<VisualElement>("productionLine2");
        productionLine2.style.display = DisplayStyle.Flex;

        // get text like InnerHTML printed on panel
        boton.text = "CLICKED";

        Debug.Log("CLICK FUNCIONO CTM");

        boton.style.unityFontStyleAndWeight = FontStyle.Bold;
        // boton.style.display = DisplayStyle.None;
    }

    private void CollectLine1()
    {
        var root = panelRend.visualTree;

        // Elements
        var collectBtn1 = root.Q<Button>("collect1");
        var productionValue1 = root.Q<Label>("productionValue1");
        var storageValue = root.Q<Label>("storageValue");

        // state Vars
        var storageValueInt = int.Parse(productionValue1.text) + int.Parse(storageValue.text);

        storageValue.text = storageValueInt.ToString();
        productionValue1.text = "0";

        collectBtn1.style.display = DisplayStyle.None;
    }

    // ------------------------------   Progress Functions   -------------------------------
    // Progress Bars functions
    void ProductionLineUp1()
    {
        // DOM
        var root = panelRend.visualTree;

        // Elements
        var collectBtn1 = root.Q<Button>("collect1");

        var productionValue1 = root.Q<Label>("productionValue1");

        // state Vars
        var productionValueInt1 = int.Parse(productionValue1.text) + 10;

        if (productionValueInt1 == 100)
        {
            // productionValue1.text = "FULL " + productionValueInt1.ToString();
            productionValue1.text = productionValueInt1.ToString();
            collectBtn1.style.display = DisplayStyle.Flex;
        }
        else
        {
            productionValue1.text = productionValueInt1.ToString();
            // Debug.Log(goldValueInt);
        }

    }

    // Progress Line - 1 production

    void GoldUp()
    {
        // DOM
        var root = panelRend.visualTree;

        // Elements
        var goldValue = root.Q<Label>("goldValue");
        var goldLabel = root.Q<Label>("goldLabel");

        // state Vars
        var goldValueInt = int.Parse(goldValue.text) + 1;

        if (goldValueInt == 100)
        {
            goldValue.text = "100 FULL " + goldValueInt.ToString();
        }
        else
        {
            goldValue.text = goldValueInt.ToString();
            // Debug.Log(goldValueInt);
        }

    }

}


