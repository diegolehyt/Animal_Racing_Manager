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

            // close window button
            var closeFactoryBtn1 = root.Q<Button>("closeFactoryBtn1");
            var openFactoryBtn1 = root.Q<Button>("openFactoryBtn1");

            if (collectBtn1 != null)
            {
                collectBtn1.clickable.clicked += CollectLine1;
                collectBtn1.clickable.clicked += CollectLine2;
            }

            if (closeFactoryBtn1 != null)
            {
                closeFactoryBtn1.clickable.clicked += CloseFactory1;
                
            }

            if (openFactoryBtn1 != null)
            {
                openFactoryBtn1.clickable.clicked += OpenFactory1;

            }

            if (boton != null)
            {
                boton.clickable.clicked += Clickable_clicke;
            }

            // TIMERS ("functionName", when to start, interval time)

            // Invoke("GoldUp", 2f);
            InvokeRepeating("GoldUp", 1f, 1f);

            InvokeRepeating("ProductionLineUp1", 1f, 1f);
            InvokeRepeating("ProductionLineUp2", 1f, 1f);
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

        // collectBtn1.style.display = DisplayStyle.None;
    }

    private void CollectLine2()
    {
        var root = panelRend.visualTree;

        // Elements
        var collectBtn1 = root.Q<Button>("collect1");
        var productionValue2 = root.Q<Label>("productionValue2");
        var storageValue = root.Q<Label>("storageValue");
        var storageValueOut = root.Q<Label>("storageValueOut");

        // state Vars
        var storageValueInt = int.Parse(productionValue2.text) + int.Parse(storageValue.text);

        storageValue.text = storageValueInt.ToString();
        storageValueOut.text = storageValueInt.ToString();
        productionValue2.text = "0";

        // collectBtn1.style.display = DisplayStyle.None;
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
            // collectBtn1.style.display = DisplayStyle.Flex;

        }

        else if (productionValueInt1 > 100)
        {
            // stop collecting at LIMIT
            return;
        }

        else
        {
            productionValue1.text = productionValueInt1.ToString();
            // Debug.Log(goldValueInt);
        }

    }

    void ProductionLineUp2()
    {
        // DOM
        var root = panelRend.visualTree;

        // Elements
        // var collectBtn2 = root.Q<Button>("collect2");

     
        var productionValue2 = root.Q<Label>("productionValue2");

        // take out
        var productionValue1 = root.Q<Label>("productionValue1");
        var storageValOut1 = root.Q<Label>("storageValOut1");
        var storageValOutInt1 = int.Parse(storageValOut1.text);

        // state Vars
        var productionValueInt2 = int.Parse(productionValue2.text) + 10;

        //take out
        var productionValueInt1 = int.Parse(productionValue1.text);

        // if (storageValOutInt1 == 200)
        if (productionValueInt2 == 100)
        {
            // productionValue1.text = "FULL " + productionValueInt1.ToString();
            productionValue2.text = productionValueInt2.ToString();
            // collectBtn2.style.display = DisplayStyle.Flex;
        }

        else if (productionValueInt2 > 100)
        {
            // stop collecting at LIMIT
            return;
        }

        else
        {
            productionValue2.text = productionValueInt2.ToString();

            // take out +20
            var storageSum = productionValueInt1 + productionValueInt2 + 20;
            storageValOut1.text = storageSum.ToString();
            // Debug.Log(goldValueInt);
        }

    }
    // Open Factory 1
    void OpenFactory1()
    {
        // DOM
        var root = panelRend.visualTree;

        // Elements
        var factoryPage1 = root.Q<VisualElement>("factoryPage1");
        var factoryObj1 = root.Q<VisualElement>("factoryObj1");

        // it hides the factory 1 window
        factoryPage1.style.display = DisplayStyle.Flex;
        factoryObj1.style.display = DisplayStyle.None;
    }

    // Close factory 1
    void CloseFactory1()
    {
        // DOM
        var root = panelRend.visualTree;

        // Elements
        var factoryPage1 = root.Q<VisualElement>("factoryPage1");
        var factoryObj1 = root.Q<VisualElement>("factoryObj1");

        // it hides the factory 1 window
        factoryPage1.style.display = DisplayStyle.None;
        factoryObj1.style.display = DisplayStyle.Flex;
    }

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


