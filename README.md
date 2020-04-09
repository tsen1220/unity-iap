# Unity IAP

This is Unity IAP/IAB from UnityEngine.Purchasing.

# 1.

Go Window > General > Services  to set your project. Then enable the In-App Purchasing.

Import the package and enter your public key from google in Options.

After finishing import, click `I Made a backup. Go Ahead!` to complete this step.

# 2.

Create class with inheriting `IStoreListener`.

The next step is initialized the listener.

We need to add prouctionId and productType into builder.

And check the initialization is completed.

```
using UnityEngine;
using UnityEngine.Purchasing;

public class SquarewarIAP : MonoBehaviour, IStoreListener
{
	private IStoreController controller;
	private IExtensionProvider extensions;

	private void Start()
	{
		InitPurchasing();
	}

	private void InitPurchasing()
	{
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
	builder.AddProduct("ProductId", ProductType.Comsuable)
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
	
	private bool isInit()
	{
		return controller != null && extensions != null;
	}
}
```

# 3.

We create a purchasing method to buy product.

Need to check that initialization is completed.

```
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
```

# 4.

When purchasing finished, listener will call success or failure methods.

Success Args contain receipts. We can save receipts data and send to server with API.

Success:
```
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		Debug.Log("Purchase Succeeded.");
		Debug.Log(e.purchasedProduct.receipt);
		return PurchaseProcessingResult.Complete;
	}
```

Failure:
```
	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		Debug.Log("Purchase Failed.");
	}
```
