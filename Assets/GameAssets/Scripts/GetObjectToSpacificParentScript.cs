using Pinpin.Scene.MainScene;
using Pinpin.Scene.MainScene.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectToSpacificParentScript : MonoBehaviour
{
    public enum Type 
    {
    Coins,
    Explosion,
    Unlock
    }
    private FeedbackManager feedbackManager;
    private ParticleSystem PS;
    public Type type;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        PS = GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(0.32f);
        feedbackManager = FindAnyObjectByType<FeedbackManager>();
        switch (type)
        {
            case Type.Coins:
                feedbackManager.m_collectCoinsParticles = PS;
                break;

            case Type.Explosion:
                if (PS != null)
                    PS.Play();
                feedbackManager.m_collectCoinsExplosionParticles = PS;
                break;

            case Type.Unlock:
                feedbackManager.m_characterUnlockParticles = PS;
                break;

            default:
                Debug.LogWarning("Unhandled type: " + type);
                break;
        }
        //yield return MainSceneUIManager.Instance.WaitForPanelsToLoad();

        if (type==Type.Explosion)
        {
            feedbackManager.m_collectCoinsExplosionParticlesBurst = feedbackManager.m_collectCoinsExplosionParticles.emission.GetBurst(0);
        }
        if (type==Type.Coins)
        {
            feedbackManager.m_collectCoinsEmission = feedbackManager.m_collectCoinsParticles.emission;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
