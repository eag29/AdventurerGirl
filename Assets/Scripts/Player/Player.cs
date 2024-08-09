using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private static Player ornek;

    public static Player Ornek
    {
        get
        {
            if (ornek == null)
            {
                ornek = GameObject.FindObjectOfType<Player>();
            }
            return ornek;
        }
    }
    public Rigidbody2D myrgb
    {
        get;
        set;
    }

    public bool Atak
    {
        get;
        set;
    }
    public bool Kayma
    {
        get;
        set;
    }
    public bool Zipla
    {
        get;
        set;
    }
    public bool ZeminUstunde
    {
        get;
        set;
    }
    [SerializeField]
    private float hiz;

    private Animator anm;

    private bool sagabak;

    private int count;
    public Text txtskor;
    public Text txtcan;

    [SerializeField]
    private GameObject anahtar;
    [SerializeField]
    private GameObject cizelge;
    [SerializeField]
    private GameObject kapi;
    [SerializeField]
    private GameObject kapii;
    [SerializeField]
    private GameObject mermiprefab;
    private int can;


    [SerializeField]
    private Transform[] Temasnoktasi;
    [SerializeField]
    private float temasCapi;
    [SerializeField]
    private LayerMask hangiZemin;

    [SerializeField]
    private bool havadaKontrol;
    [SerializeField]
    private float ziplamaKuvveti;

    [SerializeField]
    private AudioSource coinsound;
    [SerializeField]
    private AudioSource kalpsound;
    [SerializeField]
    private AudioSource boxsound;
    [SerializeField]
    private AudioSource keysound;
    [SerializeField]
    private AudioSource enemysound;
    [SerializeField]
    private AudioSource birdsound;

    void Start()
    {
        hiz = 10;
        sagabak = true;
        count = 0;
        myrgb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
    }

    void Update()
    {
        Controller();
    }
    void FixedUpdate()
    {
        float yatay = Input.GetAxis("Horizontal");
        ZeminUstunde = Zeminde();
        TemelHareketler(yatay);
        YonCevir(yatay);
        HareketKatmanlari();
    }
    private void TemelHareketler(float yatay)
    {
        if (myrgb.velocity.y < 0)
        {
            anm.SetBool("dusme", true);
        }
        if (!Atak && !Kayma && (ZeminUstunde || havadaKontrol))
        {
            myrgb.velocity = new Vector2(yatay * hiz, myrgb.velocity.y);
        }
        if (Zipla && myrgb.velocity.y == 0)
        {
            myrgb.AddForce(new Vector2(0, ziplamaKuvveti));
        }
        anm.SetFloat("karakterhiz", Mathf.Abs(yatay * hiz));
    }
    private void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anm.SetTrigger("bicakcekme");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            anm.SetTrigger("kayma");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anm.SetTrigger("zipla");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            anm.SetTrigger("ates");
            MermiFirlat(0);
        }
    }
    private void YonCevir(float yatay)
    {
        if (yatay > 0 && !sagabak || yatay < 0 && sagabak)
        {
            sagabak = !sagabak;
            Vector3 yon = transform.localScale;
            yon.x *= -1;
            transform.localScale = yon;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "box" &&  Player.Ornek.Kayma)
        {
            other.gameObject.SetActive(false);
            anahtar.SetActive(true);
            boxsound.Play();
        }
        if (other.gameObject.tag == "key")
        {
            other.gameObject.SetActive(false);
            cizelge.SetActive(true);
            kapi.SetActive(true);
            kapii.SetActive(true);
            keysound.Play();
        }
        if (other.gameObject.tag == "blok")
        {
            other.gameObject.SetActive(false);
            count = count + 10;
            SkorAyarla(count);
            coinsound.Play();
        }
        if (other.gameObject.tag == "candusur")
        {
            can = can - 1;
            canhesapla(can);
            enemysound.Play();
        }
        if (other.gameObject.tag == "candusur" && Player.Ornek.Atak)
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag =="health")
        {
            other.gameObject.SetActive(false);
            can = can + 1;
            kalpsound.Play();
        }
        if (other.gameObject.tag == "bird")
        {
            birdsound.Play();
        }
    }
    void SkorAyarla(int count)
    {
        txtskor.text = "Skor : " + count.ToString();
    }
    void canhesapla(int count)
    {
        txtcan.text = "Can :" + count.ToString();
    }
    private bool Zeminde()
    {
        if (myrgb.velocity.y <= 0)
        {
            foreach (Transform nokta in Temasnoktasi)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(nokta.position, temasCapi, hangiZemin);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void HareketKatmanlari()
    {
        if (ZeminUstunde)
        {
            anm.SetLayerWeight(1, 1);
        }
        else
        {
            anm.SetLayerWeight(1, 0);
        }
    }
    public void MermiFirlat(int value)
    {
        if (sagabak)
        {
            GameObject klonmermi = (GameObject)Instantiate(mermiprefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            klonmermi.GetComponent<mermi>().Initialize(Vector2.right);
        }
        else
        {
            GameObject klonmermi = (GameObject)Instantiate(mermiprefab, transform.position, Quaternion.Euler(new Vector3(0, -190, 0)));
            klonmermi.GetComponent<mermi>().Initialize(Vector2.left);
        }
    }
}