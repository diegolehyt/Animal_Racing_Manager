using System.Collections;
using System.Collections.Generic;
using Unity.UIElements.Runtime;
using UnityEngine.UIElements;
using UnityEngine;

public class FactoryCopy : MonoBehaviour
{

    private PanelRenderer panelRend;
    
    private void OnEnable()
    {
        panelRend = GetComponent<PanelRenderer>();

        if (panelRend != null)
            panelRend.postUxmlReload += onLoadUxml; 
    }

    private IEnumerable<Object> onLoadUxml()
    {
        if (panelRend != null)
        {
            var root = panelRend.visualTree;

            var nombre = root.Q<Label>("nombre");
            var boton = root.Q<Button>("boton");

            if (boton != null)
            {
                boton.clickable.clicked += Clickable_clicked;
            }
        }
        return null;
    }

    private void Clickable_clicked()
    {
        var root = panelRend.visualTree;
        var boton = root.Q<Button>("boton");
        Debug.Log("CLICK FUNCIONO CTM");

        boton.style.unityFontStyleAndWeight = FontStyle.Bold;
        // boton.style.display = DisplayStyle.None;
    }
    
}

// STUDY


// ------------  Sample Parse int to str  -------------
// var numero = 2;
// var stri = "5";
// var nueva = int.Parse(stri) + numero;
// numero = nueva;


// get text like InnerHTML printed on panel
// boton.text = numero.ToString();


