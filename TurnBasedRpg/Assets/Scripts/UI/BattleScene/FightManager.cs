using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FightState
{
    START,
    ENEMYTURN,
    PLAYERTURN,
    WON,
    LOST
}

public class FightManager : MonoBehaviour
{
    public FightState state;

    public Text roundDisplayText;
    public float currentRound;

    public GameObject playerPrefab;
    public List<GameObject> enemyPrefabs;

    public Sprite[] enemyImages;

    #region UI
    [Header("UI")]
    public FightPanelsManager panelsManager;
    #endregion

    #region Player Elements
    [Header("Player Elements")]
    public Transform playerBattleStation;
    private PlayerUnit playerUnit;
    public Text playerNameText;
    public Text playerLevelText;
    public Slider playerHPSlider;
    public Slider playerMPSlider;
    public BattleHUD playerHUD;
    public GameObject playerTurnIndicator;
    public GameObject[] skillButtons;
    #endregion

    #region Enemy Elements
    [Header("Enemy Elements")]
    public Transform enemyBattleStation;
    private EnemyUnit enemyUnit;
    public Text enemyNameText;
    public Text enemyLevelText;
    public Slider enemyHPSlider;
    public Slider enemyMPSlider;
    public BattleHUD enemyHUD;
    public GameObject enemyTurnIndicator;
    #endregion

    private void Start()
    {
        state = FightState.START;
        StartCoroutine(InitializeFight());

        currentRound = 1;
        roundDisplayText.text = $"Round: {currentRound}";
    }

    #region Initialization
    private IEnumerator InitializeFight()
    {
        InitializePlayer();
        InitializeEnemy();
        
        UpdateHUDS();

        yield return new WaitForSeconds(2f);

        CalculateFirstTurn();
    }

    private void InitializePlayer()
    {
        playerUnit = Instantiate(playerPrefab, playerBattleStation.position, playerBattleStation.rotation).GetComponent<PlayerUnit>();

        playerUnit.GetComponent<PlayerUnit>().OnLevelUp += OnPlayerLevelUp;

        var playerData = PlayerDataTransfer.LoadPlayerData();

        playerUnit.unitName = playerData.unitName;
        playerUnit.unitLevel = playerData.unitLevel;
        playerUnit.unitPower = playerData.unitPower;
        playerUnit.unitSpeed = playerData.unitSpeed;
        playerUnit.maxHP = playerData.maxHP;
        playerUnit.currentHP = playerData.maxHP;
        playerUnit.maxMP = playerData.maxMP;
        playerUnit.currentMP = 0;
        playerUnit.currentExp = playerData.currentExp;
        playerUnit.expToLevel = playerData.expToLevel;
        playerUnit.totalGold = playerData.totalGold;
        playerUnit.availableStatPoints = playerData.availableStatPoints;
        playerUnit.availableSkillPoints = playerData.availableSkillPoints;

        playerUnit.knownSkills = playerData.knownSkills;
        playerUnit.equippedSkills = playerData.equippedSkills;

        for (int i = 0; i < playerUnit.equippedSkills.Count; i++)
        {
            skillButtons[i].SetActive(true);
            Button skillButton = skillButtons[i].GetComponent<Button>();
            var buttonText = skillButton.GetComponentInChildren<Text>();
            if(buttonText != null)
            {
                buttonText.text = playerUnit.equippedSkills[i].SkillName;
            }
        }

        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);
    }

    private void InitializeEnemy()
    {
        List<int> availableBosses = new List<int>();

        foreach (var enemy in enemyPrefabs)
        {
            Unit boss = enemy.GetComponent<Unit>();
            if (boss.unitLevel <= playerUnit.unitLevel + 2 && boss.unitLevel >= playerUnit.unitLevel - 3)
            {
                availableBosses.Add(enemyPrefabs.IndexOf(enemy));
            }
        }

        int bossPos = Random.Range(0, availableBosses.Count);

        enemyUnit = Instantiate(enemyPrefabs[availableBosses[bossPos]], enemyBattleStation.position, enemyBattleStation.rotation).GetComponent<EnemyUnit>();
        enemyUnit.SetBossStats();
        enemyUnit.SetBossSkills();

        Debug.Log(enemyUnit.equippedSkills[0].SkillName);

        enemyHUD.SetHP(enemyUnit.currentHP);
        enemyHUD.SetMP(enemyUnit.currentMP);
    }

    private void CalculateFirstTurn()
    {
        if(playerUnit.unitSpeed <= enemyUnit.unitSpeed)
        {
            state = FightState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            state = FightState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
    }
    #endregion

    #region Attack Buttons
    public void NormalAttack_Button()
    {
        if(state != FightState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerNormalAttack());
    }

    public void SkillOne_Button()
    {
        if(state != FightState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerSkillOneAttack());
    }

    public void SkillTwo_Button()
    {
        if (state != FightState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerSkillTwoAttack());
    }

    public void SkillThree_Button()
    {
        if (state != FightState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerSkillThreeAttack());
    }
    #endregion

    #region Attacks
    private IEnumerator PlayerNormalAttack()
    {
        state = FightState.ENEMYTURN;

        bool isDead = BattleLogicManager.PerformNormalAttack(playerUnit, enemyUnit);

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            playerUnit.GainExperience(enemyUnit.rewardExp);
            playerUnit.GainGold(enemyUnit.rewardGold);
            EndFight(playerUnit, enemyUnit, 1);
        }
        else
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.currentMP);
            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerSkillOneAttack()
    {
        state = FightState.ENEMYTURN;

        Debug.Log(playerUnit.equippedSkills[0].SkillName);

        bool isDead = BattleLogicManager.AttackWithSkill(playerUnit, enemyUnit, playerUnit.equippedSkills[0]);

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            playerUnit.GainExperience(enemyUnit.rewardExp);
            playerUnit.GainGold(enemyUnit.rewardGold);
            EndFight(playerUnit, enemyUnit, 1);
        }
        else
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.currentMP);
            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerSkillTwoAttack()
    {
        state = FightState.ENEMYTURN;

        bool isDead = BattleLogicManager.AttackWithSkill(playerUnit, enemyUnit, playerUnit.equippedSkills[1]);

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            playerUnit.GainExperience(enemyUnit.rewardExp);
            playerUnit.GainGold(enemyUnit.rewardGold);
            EndFight(playerUnit, enemyUnit, 1);
        }
        else
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.currentMP);
            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerSkillThreeAttack()
    {
        state = FightState.ENEMYTURN;

        bool isDead = BattleLogicManager.AttackWithSkill(playerUnit, enemyUnit, playerUnit.equippedSkills[2]);

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            playerUnit.GainExperience(enemyUnit.rewardExp);
            playerUnit.GainGold(enemyUnit.rewardGold);
            EndFight(playerUnit, enemyUnit, 1);
        }
        else
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.currentMP);
            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            StartCoroutine(EnemyTurn());
        }
    }
    #endregion

    #region Turns
    private void EndFight(Unit winner, Unit loser, int fightCode)
    {
        //set up end fight stuff
        //pop ups, menus, text, etc.

        panelsManager.EndFightRoutine(winner, loser, fightCode);
    }

    private IEnumerator PlayerTurn()
    {
        currentRound += 0.5f;
        enemyTurnIndicator.SetActive(false);
        playerTurnIndicator.SetActive(true);
        yield return new WaitForSeconds(1f);

        roundDisplayText.text = $"Round: {(int)currentRound}";
    }
    private IEnumerator EnemyTurn()
    {
        //enemy ai goes here
        enemyTurnIndicator.SetActive(true);
        playerTurnIndicator.SetActive(false);
        currentRound += 0.5f;

        yield return new WaitForSeconds(1f);

        bool isDead = false;
        bool skillUsed = false;

        foreach (var skill in enemyUnit.equippedSkills)
        {
            if (enemyUnit.currentMP >= skill.EnergyCost && skillUsed == false)
            {
                int attackChoice = Random.Range(0, 2);

                if(attackChoice == 1)
                {
                    skillUsed = true;
                    isDead = BattleLogicManager.AttackWithSkill(enemyUnit, playerUnit, skill);
                }
            }
        }

        if (skillUsed == false)
        {
            isDead = BattleLogicManager.PerformNormalAttack(enemyUnit, playerUnit);
        }

        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetMP(enemyUnit.currentMP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = FightState.LOST;
            EndFight(enemyUnit, playerUnit, 0);
        }
        else
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.currentMP);
            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            state = FightState.PLAYERTURN;
            StartCoroutine(PlayerTurn());

            roundDisplayText.text = $"Round: {(int)currentRound}";
        }
    }
    #endregion

    #region HUDs
    private void UpdateHUDS()
    {
        playerHUD.UpdateHUD(playerUnit);
        enemyHUD.UpdateHUD(enemyUnit);
    }

    private void OnPlayerLevelUp(object sender, int eventCode)
    {
        panelsManager.GetComponent<FightPanelsManager>().levelUpIndicator.SetActive(true);
    }

    public void Flee_Button()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

    #region IO
    public void SavePlayerData()
    {
        PlayerDataTransfer.SavePlayerData(playerUnit);
    }
    #endregion
}
