using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChemicalFormulas;

public class CompoundRepository : BaseRepository<ChemicalFormulas.CompoundSO>
{
    public CompoundRepository(List<ChemicalFormulas.CompoundSO> compounds) 
        : base(compounds, compound => compound.Compound.getformula, compound => compound.Quantity)
    {
    }
}