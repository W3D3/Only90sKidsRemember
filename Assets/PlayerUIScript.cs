using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    public Player Player;
    public Text PlayerName;
    public Text AmmoText;
    public GameObject Live1;
    public GameObject Live2;
    public GameObject Dead;

    public Image ItemImage;

    public ThrowScript Throw;

    public Sprite DefaultItemImage;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            gameObject.SetActive(false);
            return;
        }

        PlayerName.text = Player.Name;
        Throw = Player.GetComponent<ThrowScript>();

        Live2.SetActive(true);
        Live1.SetActive(false);
        Dead.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.health == 1)
        {
            Live2.SetActive(false);
            Live1.SetActive(true);
        } else if (Player.health <= 0)
        {
            Live1.SetActive(false);
            Dead.SetActive(true);
        }

        if (Throw.SpecialWeapon == null)
        {
            ItemImage.sprite = DefaultItemImage;
        }
        else
        {
            ItemImage.sprite = Throw.SpecialWeapon.Thumbnail;
        }

        AmmoText.text = $"{Throw.PrimaryAmmo}";
    }
}
