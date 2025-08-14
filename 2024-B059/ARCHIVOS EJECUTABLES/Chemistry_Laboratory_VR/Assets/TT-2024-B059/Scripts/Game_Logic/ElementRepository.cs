using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChemicalFormulas;

public class ElementRepository : BaseRepository<ChemicalFormulas.Element>
{
    public ElementRepository(List<ChemicalFormulas.Element> elements) 
        : base(elements, element => element.ElementSO.getformula, element => element.Quantity)
    {
    }
}