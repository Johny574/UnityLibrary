


using UnityEngine;

public class Shadow2D : MonoBehaviour {
   [SerializeField] Transform _cells, _shadow;
    [SerializeField] float _offset =1f;

    void Awake() {
        CreateShadows();
    }
    
   void CreateShadows() {
       foreach (Transform cell in _cells)
       {
           var shadow = Instantiate(cell, cell.position, Quaternion.identity, _shadow);
           shadow.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
           shadow.GetComponent<SpriteRenderer>().sortingOrder = 0;
        //    shadow.transform.position = ()shadow.transform.position + _offset;
           shadow.localScale = cell.localScale;
       }
   }

    void Update() {

        for (int i = 0; i < _shadow.childCount; i++)
        {
            var child = _shadow.GetChild(i);
            Vector3 dif = transform.position - GlobalLight2D.Instance.GlobalLightSource.transform.position;
            float angle = Rotation2D.LookAngle(dif);
            child.localPosition = Rotation2D.GetPointOnCircle(angle) * _offset;
            child.localEulerAngles = new Vector3(0f, 0f, angle-90);
        }
    } 
}