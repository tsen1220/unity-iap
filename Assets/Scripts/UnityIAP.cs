using UnityEngine;
using UnityEngine.Purchasing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnityIAP : MonoBehaviour, IStoreListener
{
	private IStoreController controller;
	private IExtensionProvider extensions;
	public Button[] buttons;

	private void Start()
	{
		InitPurchasing();
	}

	private void InitPurchasing()
	{
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		foreach (Button btn in buttons)
		{
			builder.AddProduct(btn.GetComponent<ButtonClass>().productId, btn.GetComponent<ButtonClass>().productType);
		}

		UnityPurchasing.Initialize(this, builder);
	}


	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("IAP InitializeFailed:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		Debug.Log("Purchase Succeeded.");
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		Debug.Log("Purchase Failed.");
	}

	public void BuyProductionWithId(string productionId)
	{
		if (isInit())
		{
			Product product = controller.products.WithID(productionId);
			if (product.availableToPurchase && product != null)
			{
				controller.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("This merchandise is not exist.");
			}
		}
		else
		{
			Debug.Log("Need to initialize.");
			InitPurchasing();
		}
	}

	private bool isInit()
	{
		return controller != null && extensions != null;
	}
}
