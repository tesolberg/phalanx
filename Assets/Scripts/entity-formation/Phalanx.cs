using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;

public class Phalanx
{

    // TODO: Phalanxen bør ta over setup av formasjonen fra formation generator. Det kan internt i phalanxen genereres en første rank som så spes på 
    // bakover. Linkene kan legges i lister for hver kolonne. Da kan det kjøres utjevningssjekker.

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public List<PhalanxLink> links = new List<PhalanxLink>();


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    private List<List<PhalanxLink>> rows = new List<List<PhalanxLink>>();

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void AddLink(PhalanxLink link)
    {
        if (!links.Contains(link)) links.Add(link);
    }

    public void RemoveLink(PhalanxLink link)
    {
        if (links.Contains(link))
        {
            link.OnLinkRemovedFromPhalanx();

            links.Remove(link);
        }
    }

    public void Disband()
    {
        foreach (PhalanxLink link in links)
        {
            Entity entity = link.GetComponent<Entity>();
            entity.ActivePhalanx = null;
            entity.SelectEntityAsPhalanxMember(false);
        }
    }

    public void SetSelected(bool selected)
    {
        foreach (PhalanxLink link in links)
        {
            link.GetComponent<Entity>().SelectEntityAsPhalanxMember(selected);
        }
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////


}
