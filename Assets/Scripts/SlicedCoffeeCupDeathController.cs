using UnityEngine;

public class SlicedCoffeeCupDeathController: MonoBehaviour {
    private void Start() {
        var i = 0;
        while (true) {
            try {
                var child = transform.GetChild(i).gameObject;
                var rigidbody = child.GetComponent<Rigidbody>();
                rigidbody.velocity = new Vector3(0, 0, UnityEngine.Random.Range(10f, 15f));
                i++;
            } catch {
                break;
            }
        }
    }
}