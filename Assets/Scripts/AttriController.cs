using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttriController : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _mpBar;
    [SerializeField] private Slider _staminaBar;
    private Knight knight;
    private float smoothSpeed = 3f;
    private Coroutine _hpCoroutine;
    private Coroutine _mpCoroutine;
    private Coroutine _staminaCoroutine;

    void Awake()
    {
        knight = FindFirstObjectByType<Knight>();
        _hpBar.value = 0f;
        _mpBar.value = 0f;
        _staminaBar.value = 0f;
    }

    void Start()
    {
        // Start coroutines to fill bars
        UpdateHpBarValueUI((float)knight.GetHp() / knight.GetMaxHp());
        UpdateMpBarValueUI((float)knight.GetMp() / knight.GetMaxMp());
        UpdateStaminaBarValueUI((float)knight.GetStamina() / knight.GetMaxStamina());
    }

    public void UpdateHpBarValueUI(float targetValue)
    {
        if (_hpCoroutine != null) StopCoroutine(_hpCoroutine);
        _hpCoroutine = StartCoroutine(SmoothFill(_hpBar, targetValue));
    }

    public void UpdateMpBarValueUI(float targetValue)
    {
        if (_mpCoroutine != null) StopCoroutine(_mpCoroutine);
        _mpCoroutine = StartCoroutine(SmoothFill(_mpBar, targetValue));
    }

    public void UpdateStaminaBarValueUI(float targetValue)
    {
        if (_staminaCoroutine != null) StopCoroutine(_staminaCoroutine);
        _staminaCoroutine = StartCoroutine(SmoothFill(_staminaBar, targetValue));
    }

    IEnumerator SmoothFill(Slider bar, float targetValue)
    {
        while (!Mathf.Approximately(bar.value, targetValue))
        {
            bar.value = Mathf.MoveTowards(bar.value, targetValue, smoothSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
