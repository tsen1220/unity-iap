using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour
{
	public string productId;
	public ProductType productType;


	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => { FindObjectOfType<UnityIAP>().BuyProductionWithId(productId); });
	}
}
