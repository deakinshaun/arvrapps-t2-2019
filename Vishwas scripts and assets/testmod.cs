using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class testmod : MonoBehaviour
{
    [Test]
	public void Calculatemod_Test(){
		Choose cs=new Choose();
		int pr=17;
		int ge=3;
		int prvt=15;
		int r=((int)(Mathf.Pow(ge,prvt))%pr);
		int res=cs.modcalc(pr,ge,prvt);
		Assert.That(res,Is.EqualTo(r));
		}
}
