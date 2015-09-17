using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarbonContacter : Contacter
{
	public List<float> sizes;
	int _index;
	float _size;

	void Start(){
		_index = Random.Range(0, sizes.Count);
		_size = sizes[_index];
		transform.localScale = transform.localScale * _size;
	}

	//когда попадает в ведро - мелкие брызги и увеличение счетчика воды
	//когда ударяется об ведро - крупные брызги и ничего не начисляется
	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addCarbon (_index);
		Destroy (gameObject);
	}

	public float size{
		get{
			return _size;
		}
	}

}
