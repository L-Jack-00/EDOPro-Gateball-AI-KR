using System;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using WindBot.Game.AI.Enums;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    public class GateballBaseExecutor : DefaultExecutor
    {
        protected static Random _rand = new Random();

        public GateballBaseExecutor(GameAI ai, Duel duel) : base(ai, duel)
        {
            InitAdvancedTactics();
        }

        protected virtual void InitArchetypeTactics()
        {
            // Override in derived classes to inject archetype-specific top priority combos!
        }

        protected virtual void InitAdvancedTactics()
        {
            // --- 0. ARCHETYPE-SPECIFIC COMBOS & KEY TACTICS (Highest Priority) ---
            InitArchetypeTactics();

            // --- 1. COUNTER TRAPS & INSTANT NEGATES (Top Priority) ---
            AddExecutor(ExecutorType.Activate, 41420027, SmartSolemnJudgment);    // Solemn Judgment (Smart priority threat negation)
            AddExecutor(ExecutorType.Activate, 84749824, SmartSolemnWarning);     // Solemn Warning
            AddExecutor(ExecutorType.Activate, 40605147, DefaultSolemnStrike);      // Solemn Strike

            // --- 2. BOARD CLEARS & REMOVALS (Immediate board sweeps) ---
            AddExecutor(ExecutorType.Activate, 18144506, DefaultHarpiesFeatherDusterFirst); // Harpie's Feather Duster
            AddExecutor(ExecutorType.Activate, 19613556, SmartHeavyStorm);                  // Heavy Storm
            AddExecutor(ExecutorType.Activate, 12580477, DefaultRaigeki);                   // Raigeki
            AddExecutor(ExecutorType.Activate, 53129443, DefaultDarkHole);                  // Dark Hole

            // --- 3. HAND ADVANTAGE & DRAW SPELLS ---
            AddExecutor(ExecutorType.Activate, 55144522); // Pot of Greed
            AddExecutor(ExecutorType.Activate, 79571449); // Graceful Charity
            AddExecutor(ExecutorType.Activate, 67169062); // Pot of Avarice
            AddExecutor(ExecutorType.Activate, 98645731, SmartPotOfDuality);  // Pot of Duality
            AddExecutor(ExecutorType.Activate, 44763025, SmartDelinquentDuo); // Delinquent Duo
            AddExecutor(ExecutorType.Activate, 17375316, SmartConfiscation);  // Confiscation

            // --- 4. TARGETED REMOVALS & SPOT CONTROL ---
            AddExecutor(ExecutorType.Activate, 5318639,  SmartMST);                   // MST (Dedicated tactical quick-play targeting)
            AddExecutor(ExecutorType.Activate, 71044499, SmartNoblemanOfCrossout);     // Nobleman of Crossout
            AddExecutor(ExecutorType.Activate, 45986603, SmartSnatchSteal);           // Snatch Steal
            AddExecutor(ExecutorType.Activate, 87953373, SmartBrainControl);          // Brain Control
            AddExecutor(ExecutorType.Activate, 43422537, DefaultSmashingGround);      // Smashing Ground
            AddExecutor(ExecutorType.Activate, 26412047, DefaultHammerShot);          // Hammer Shot
            AddExecutor(ExecutorType.Activate, 66788016, DefaultSmashingGround);      // Fissure
            AddExecutor(ExecutorType.Activate, 14087893, SmartBookOfMoon);            // Book of Moon (Smart combat & flip combo)
            AddExecutor(ExecutorType.Activate, 98045062, SmartEnemyController);       // Enemy Controller (Tribute take or Battle stop)

            // --- 5. FLOODGATES & CONTINUOUS CARDS ---
            AddExecutor(ExecutorType.Activate, 82732705); // Skill Drain
            AddExecutor(ExecutorType.Activate, 51452091); // Royal Decree
            AddExecutor(ExecutorType.Activate, 86780027); // Solidarity
            AddExecutor(ExecutorType.Activate, 57953380); // Card of Safe Return
            AddExecutor(ExecutorType.Activate, 94585852); // Pandemonium

            // --- 6. REVIVALS & SPECIAL SPELLS ---
            AddExecutor(ExecutorType.Activate, 83764718, SmartMonsterReborn);     // Monster Reborn
            AddExecutor(ExecutorType.Activate, 70828912, SmartPrematureBurial);   // Premature Burial
            AddExecutor(ExecutorType.Activate, 97077563, DefaultCallOfTheHaunted);// Call of the Haunted
            AddExecutor(ExecutorType.Activate, 2204140,  SmartBookOfLife);        // Book of Life
            AddExecutor(ExecutorType.Activate, 220414,   SmartBookOfLife);
            AddExecutor(ExecutorType.Activate, 46411259, SmartMetamorphosis);     // Metamorphosis
            AddExecutor(ExecutorType.Activate, 73915051, DefaultScapegoat);       // Scapegoat (End phase / attack trigger)

            // --- 7. MONSTER EFFECTS & ARCHETYPES ---
            AddExecutor(ExecutorType.Activate, 71413901, SmartBreaker);           // Breaker
            AddExecutor(ExecutorType.Activate, 504700118, SmartBLS);              // BLS Envoy (Goat)
            AddExecutor(ExecutorType.Activate, 72989439, SmartBLS);               // BLS Envoy (Standard)
            AddExecutor(ExecutorType.Activate, 9596126,  SmartChaosSorcerer);     // Chaos Sorcerer
            AddExecutor(ExecutorType.Activate, 65192027, SmartDarkArmedDragon);   // Dark Armed Dragon
            AddExecutor(ExecutorType.Activate, 44155002, SmartRaiza);             // Raiza the Storm Monarch
            AddExecutor(ExecutorType.Activate, 73125233, SmartRaiza);
            AddExecutor(ExecutorType.Activate, 49721904, SmartCaius);             // Caius the Shadow Monarch
            AddExecutor(ExecutorType.Activate, 9748752,  SmartCaius);
            AddExecutor(ExecutorType.Activate, 4929256,  SmartMobius);            // Mobius the Frost Monarch
            AddExecutor(ExecutorType.Activate, 51945556, SmartZaborg);            // Zaborg the Thunder Monarch
            AddExecutor(ExecutorType.Activate, 12538374, SmartTreebornFrog);      // Treeborn Frog
            AddExecutor(ExecutorType.Activate, 40044918, SmartStratos);           // E-HERO Stratos
            AddExecutor(ExecutorType.Activate, 75888208, SmartGeminiSpark);       // Gemini Spark
            AddExecutor(ExecutorType.Activate, 37742478, DefaultHonestEffect);    // Honest
            AddExecutor(ExecutorType.Activate, 31560081, SmartMagicianOfFaith);   // Magician of Faith
            AddExecutor(ExecutorType.Activate, 16226786, SmartNightAssailant);    // Night Assailant
            AddExecutor(ExecutorType.Activate, 63519819, SmartTER);               // Thousand-Eyes Restrict
            AddExecutor(ExecutorType.Activate, 80344569);                         // Neo-Spacian Grand Mole (Bounce)
            AddExecutor(ExecutorType.Activate, 19740112, SmartSnipeHunterActivate);// Snipe Hunter (Discard destroy)
            AddExecutor(ExecutorType.Activate, 97268402, SmartEffectVeiler);       // Effect Veiler (Main phase negate)
            AddExecutor(ExecutorType.Activate, 98777036, SmartTragoediaActivate);  // Tragoedia (Damage SS & Steal)
            AddExecutor(ExecutorType.Activate, 70074900);                          // D.D. Assailant (Banish)
            AddExecutor(ExecutorType.Activate, 7572887);                           // D.D. Warrior Lady (Banish)

            // --- 8. DEFENSIVE TRAPS (Built-in Battle & Trigger Response) ---
            AddExecutor(ExecutorType.Activate, 53582587, SmartTorrentialTribute); // Torrential Tribute (Overextension & boss punish)
            AddExecutor(ExecutorType.Activate, 83555666, SmartRingOfDestruction);  // Ring of Destruction
            AddExecutor(ExecutorType.Activate, 44095762, DefaultUniqueTrap);       // Mirror Force
            AddExecutor(ExecutorType.Activate, 56120475, DefaultUniqueTrap);       // Sakuretsu Armor
            AddExecutor(ExecutorType.Activate, 70342110, DefaultUniqueTrap);       // Dimensional Prison
            AddExecutor(ExecutorType.Activate, 29401950, DefaultUniqueTrap);       // Bottomless Trap Hole
            AddExecutor(ExecutorType.Activate, 94192409, DefaultCompulsoryEvacuationDevice); // Compulsory Evacuation Device
            AddExecutor(ExecutorType.Activate, 55713623, SmartShrink);             // Shrink Combat Trick

            // --- 9. UNIVERSAL FALLBACKS (Guarded & Safe) ---
            AddExecutor(ExecutorType.Activate, SafeFallbackActivate); // Safe fallback (No blind quick-play fires!)

            // --- 10. SUMMONS & MONSTER SETS ---
            AddExecutor(ExecutorType.SpSummon, 70095154); // Cyber Dragon
            AddExecutor(ExecutorType.SpSummon, 83104731);
            AddExecutor(ExecutorType.SpSummon);

            AddExecutor(ExecutorType.Summon, 80344569, SmartGrandMoleSummon);     // Neo-Spacian Grand Mole (Bounce priority)
            AddExecutor(ExecutorType.Summon, 19740112, SmartSnipeHunterSummon);    // Snipe Hunter
            AddExecutor(ExecutorType.Summon, 34853266, SmartTsukuyomiSummon);     // Tsukuyomi
            AddExecutor(ExecutorType.MonsterSet, SmartMonsterSet);                // Flip forced set
            AddExecutor(ExecutorType.Summon, SmartMonsterSummon);                 // Normal summon & Overextension guard

            // --- 11. SPELL & TRAP SET ---
            AddExecutor(ExecutorType.SpellSet, SmartSpellSet);

            // --- 12. BATTLE POSITION REPOS ---
            AddExecutor(ExecutorType.Repos, SmartMonsterRepos);
        }

        // --- Tactical Implementations ---

        // --- Smart Card Selection Interceptor (Friendly Fire Prevention) ---

        protected bool IsBotCard(ClientCard c)
        {
            if (c == null) return false;
            foreach (ClientCard m in Bot.GetMonsters())
                if (m != null && m == c) return true;
            foreach (ClientCard s in Bot.GetSpells())
                if (s != null && s == c) return true;
            foreach (ClientCard h in Bot.Hand)
                if (h != null && h == c) return true;
            foreach (ClientCard g in Bot.Graveyard)
                if (g != null && g == c) return true;
            return c.Controller == 0;
        }

        protected bool IsEnemyCard(ClientCard c)
        {
            if (c == null) return false;
            return !IsBotCard(c);
        }

        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, long hint, bool cancelable)
        {
            if (cards == null || cards.Count == 0)
                return base.OnSelectCard(cards, min, max, hint, cancelable);

            // 1. Determine currently activating card ID (from Card or current chain)
            ClientCard activatingCard = Card;
            if (activatingCard == null && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
            {
                activatingCard = Duel.CurrentChain[Duel.CurrentChain.Count - 1];
            }
            int actId = activatingCard != null ? activatingCard.Id : 0;

            // [PREVENTION 1] Mystical Space Typhoon (5318639)
            // NEVER destroy bot's own spells/traps!
            if (actId == 5318639)
            {
                List<ClientCard> enemySpells = new List<ClientCard>();
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c))
                        enemySpells.Add(c);
                }

                if (enemySpells.Count > 0)
                {
                    List<ClientCard> selected = new List<ClientCard>();
                    // Priority 1: Face-up continuous / field / equip
                    foreach (ClientCard c in enemySpells)
                    {
                        if (c.IsFaceup() && (c.HasType(CardType.Continuous) || c.HasType(CardType.Field) || c.HasType(CardType.Equip)))
                        {
                            selected.Add(c);
                            if (selected.Count >= min) return selected;
                        }
                    }
                    // Priority 2: Set spells/traps
                    foreach (ClientCard c in enemySpells)
                    {
                        if (c.IsFacedown() && !selected.Contains(c))
                        {
                            selected.Add(c);
                            if (selected.Count >= min) return selected;
                        }
                    }
                    // Priority 3: Any enemy spell
                    foreach (ClientCard c in enemySpells)
                    {
                        if (!selected.Contains(c))
                        {
                            selected.Add(c);
                            if (selected.Count >= min) return selected;
                        }
                    }
                    if (selected.Count >= min) return selected;
                }

                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 2] Shrink (55713623)
            // NEVER halve attack of bot's own monsters!
            if (actId == 55713623)
            {
                List<ClientCard> enemyMonsters = new List<ClientCard>();
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c) && c.IsFaceup())
                        enemyMonsters.Add(c);
                }

                if (enemyMonsters.Count > 0)
                {
                    // Priority 1: Current battling monster
                    if (Enemy.BattlingMonster != null && enemyMonsters.Contains(Enemy.BattlingMonster))
                    {
                        return new List<ClientCard> { Enemy.BattlingMonster };
                    }

                    // Priority 2: Enemy monster with highest attack
                    ClientCard highest = null;
                    foreach (ClientCard c in enemyMonsters)
                    {
                        if (highest == null || c.Attack > highest.Attack)
                            highest = c;
                    }
                    if (highest != null)
                    {
                        return new List<ClientCard> { highest };
                    }
                }

                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 3] Book of Moon (14087893)
            if (actId == 14087893)
            {
                // A. Friendly flip recycling (Magician of Faith, Night Assailant, TER)
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsBotCard(c) && c.IsFaceup())
                    {
                        if (c.Id == 31560081 || c.Id == 16226786 || (c.Id == 63519819 && c.EquipCards.Count > 0))
                        {
                            return new List<ClientCard> { c };
                        }
                    }
                }

                // B. Enemy threat monster (never flip bot's own attackers to defense!)
                List<ClientCard> enemyMonsters = new List<ClientCard>();
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c) && c.IsFaceup())
                        enemyMonsters.Add(c);
                }

                if (enemyMonsters.Count > 0)
                {
                    if (Enemy.BattlingMonster != null && enemyMonsters.Contains(Enemy.BattlingMonster))
                        return new List<ClientCard> { Enemy.BattlingMonster };

                    ClientCard best = null;
                    foreach (ClientCard c in enemyMonsters)
                    {
                        if (best == null || c.Attack > best.Attack)
                            best = c;
                    }
                    if (best != null) return new List<ClientCard> { best };
                }

                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 4] Nobleman of Crossout (71044499)
            if (actId == 71044499)
            {
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c) && c.IsFacedown())
                        return new List<ClientCard> { c };
                }
                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 5] Snatch Steal (45986603) & Brain Control (87953373)
            if (actId == 45986603 || actId == 87953373)
            {
                ClientCard best = null;
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c) && c.IsFaceup())
                    {
                        if (best == null || c.Attack > best.Attack)
                            best = c;
                    }
                }
                if (best != null) return new List<ClientCard> { best };
                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 6] Breaker the Magical Warrior (71413901)
            if (actId == 71413901)
            {
                foreach (ClientCard c in cards)
                {
                    if (c != null && IsEnemyCard(c))
                        return new List<ClientCard> { c };
                }
                if (cancelable) return new List<ClientCard>();
            }

            // [PREVENTION 7] Universal Friendly-Fire Safety Fallback:
            // If base.OnSelectCard produces null or fails, default to enemy cards first!
            IList<ClientCard> defaultResult = base.OnSelectCard(cards, min, max, hint, cancelable);
            if (defaultResult != null && defaultResult.Count >= min)
            {
                return defaultResult;
            }

            List<ClientCard> fallbackEnemy = new List<ClientCard>();
            foreach (ClientCard c in cards)
            {
                if (c != null && IsEnemyCard(c))
                {
                    fallbackEnemy.Add(c);
                    if (fallbackEnemy.Count >= min) return fallbackEnemy;
                }
            }

            if (fallbackEnemy.Count > 0)
                return fallbackEnemy;

            return base.OnSelectCard(cards, min, max, hint, cancelable);
        }

        // --- Smart Battle Targeting (Suicide Prevention & Defense Consideration) ---

        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            if (attacker == null) return null;

            // 1. 공격력이 0인 벽 몬스터(스피릿 리퍼, 마슈마론 등)는 절대 공격하지 않음!
            if (attacker.Attack == 0 || attacker.Id == 23205979 || attacker.Id == 31305911)
                return null;

            // 2. 공격 불가능 상태 체크
            if (!attacker.IsAttack() || attacker.Attacked)
                return null;

            // 3. 상대 필드에 몬스터가 없는 경우 -> 직접 공격
            if (defenders == null || defenders.Count == 0)
            {
                if (attacker.CanDirectAttack)
                    return AI.Attack(attacker, null);
                return null;
            }

            // 4. 상대 몬스터 타겟팅 지능화
            // (0) 네오스페이시언 그랜드몰 (Neo-Spacian Grand Mole, 80344569):
            // 데미지 스텝 개시 시 데미지 계산 없이 상대 몬스터와 함께 패로 바운스되므로 무적의 제거기!
            // 상대의 가장 강력한 몬스터(융합/싱크로, 파괴 내성 벽, 고타점 몬스터)를 최우선 타겟팅!
            if (attacker.Id == 80344569)
            {
                ClientCard bestMoleTarget = null;
                // 1순위: 융합/싱크로 몬스터, 파괴 내성(리퍼/마슈마론), 고타점(1500+) 몬스터
                foreach (ClientCard d in defenders)
                {
                    if (d != null && d.IsFaceup())
                    {
                        if (d.HasType(CardType.Synchro) || d.HasType(CardType.Fusion) ||
                            d.Id == 23205979 || d.Id == 31305911 || d.Attack >= 1500 || d.IsMonsterDangerous())
                        {
                            if (bestMoleTarget == null || d.Attack > bestMoleTarget.Attack)
                                bestMoleTarget = d;
                        }
                    }
                }
                // 2순위: 기타 아무 앞면 몬스터
                if (bestMoleTarget == null)
                {
                    foreach (ClientCard d in defenders)
                    {
                        if (d != null && d.IsFaceup())
                        {
                            bestMoleTarget = d;
                            break;
                        }
                    }
                }
                // 3순위: 세트 몬스터
                if (bestMoleTarget == null && defenders.Count > 0)
                {
                    bestMoleTarget = defenders[0];
                }

                if (bestMoleTarget != null)
                    return AI.Attack(attacker, bestMoleTarget);
            }

            // (1) 이길 수 있는 앞면 공격 표시 몬스터 우선 격파
            ClientCard bestAtkTarget = null;
            foreach (ClientCard d in defenders)
            {
                if (d != null && d.IsFaceup() && d.IsAttack())
                {
                    // 아군 공격력이 적 공격력보다 높은 경우에만 공격!
                    if (attacker.Attack > d.Attack)
                    {
                        // 적 공격력이 가장 높은 몬스터(가장 위협적인 몬스터) 우선 제거
                        if (bestAtkTarget == null || d.Attack > bestAtkTarget.Attack)
                            bestAtkTarget = d;
                    }
                }
            }
            if (bestAtkTarget != null)
                return AI.Attack(attacker, bestAtkTarget);

            // (2) 이길 수 있는 앞면 수비 표시 몬스터 격파
            ClientCard bestDefTarget = null;
            foreach (ClientCard d in defenders)
            {
                if (d != null && d.IsFaceup() && d.IsDefense())
                {
                    // 전투 파괴 내성 몬스터(스피릿 리퍼, 마슈마론 등)는 우선순위 낮춤
                    if (d.Id == 23205979 || d.Id == 31305911) continue;

                    // 아군 공격력이 적 수비력보다 높은 경우 격파!
                    if (attacker.Attack > d.Defense)
                    {
                        if (bestDefTarget == null || d.Defense > bestDefTarget.Defense)
                            bestDefTarget = d;
                    }
                }
            }
            if (bestDefTarget != null)
                return AI.Attack(attacker, bestDefTarget);

            // (3) 뒷면 수비 표시 몬스터 적극 공격 (수비 몬스터 공격 거부 현상 완벽 박멸)
            // 공격력 1000 이상의 몬스터(가제트 1200~1400, 토마토 1400, 여전사 1500, 오션 1600, 에어맨 1800 등)는
            // 상대의 세트 몬스터를 주저 없이 공격하여 상대의 벽/세트를 돌파!
            if (attacker.Attack >= 1000 || attacker.Id == 7572887)
            {
                foreach (ClientCard d in defenders)
                {
                    if (d != null && d.IsFacedown())
                    {
                        return AI.Attack(attacker, d);
                    }
                }
            }

            // (4) 파괴 내성 앞면 수비 몬스터(스피릿 리퍼 등) 타격 (다른 표적이 없을 때)
            foreach (ClientCard d in defenders)
            {
                if (d != null && d.IsFaceup() && d.IsDefense())
                {
                    if (attacker.Attack > d.Defense)
                    {
                        return AI.Attack(attacker, d);
                    }
                }
            }

            // (5) 동귀어진(자폭 공격) 신중 판단:
            // 공격력이 같은 적 앞면 공격 몬스터가 있을 때,
            // 아군이 토큰이거나 유언계(킬러 토마토, 샤이닝 엔젤, 피라미드 터틀, 이차원의 여전사 등)인 경우에만 자폭 허용!
            foreach (ClientCard d in defenders)
            {
                if (d != null && d.IsFaceup() && d.IsAttack() && attacker.Attack == d.Attack)
                {
                    bool isFloatCard = attacker.Id == 77044671 || // Pyramid Turtle
                                       attacker.Id == 18807108 || // Shining Angel
                                       attacker.Id == 27288416 || // Mystic Tomato
                                       attacker.Id == 7572887  || // D.D. Warrior Lady
                                       attacker.Id == 74131780 || // Exiled Force
                                       attacker.HasType(CardType.Token);
                    if (isFloatCard)
                    {
                        return AI.Attack(attacker, d);
                    }
                }
            }

            // (6) 이차원의 여전사(D.D. Warrior Lady):
            // 상대 앞면 공격 몬스터가 강력한 보스(2000+)일 경우 제외를 위해 자폭 공격 허용
            if (attacker.Id == 7572887)
            {
                ClientCard highestEnemy = Util.GetBestEnemyMonster();
                if (highestEnemy != null && highestEnemy.IsFaceup() && highestEnemy.Attack >= 2000)
                {
                    return AI.Attack(attacker, highestEnemy);
                }
            }

            // (7) 안전 회피:
            // 상대 몬스터가 모두 아군보다 강하여 격파할 수 없는 경우,
            // 무모하게 들이받지 않고 안전하게 메인 2로 이양 (GameAI의 무조건 돌진 폴백 원천 차단!)
            if (Duel.Phase == DuelPhase.Battle || Duel.Phase == DuelPhase.BattleStep || Duel.Phase == DuelPhase.BattleStart)
            {
                return AI.ToMainPhase2();
            }

            return null;
        }

        protected bool SmartTsukuyomiSummon()
        {
            // Golden Rule: Tsukuyomi mandatory effect requires face-up targets.
            // 1. Combo with Bot's own monsters (recycle flip effects or TER absorption):
            foreach (ClientCard m in Bot.GetMonsters())
            {
                if (m != null && m.IsFaceup())
                {
                    if (m.Id == 31560081 || m.Id == 16226786) // Magician of Faith or Night Assailant
                    {
                        AI.SelectCard(m);
                        return true;
                    }
                    if (m.Id == 63519819 && m.EquipCards.Count > 0) // TER with equipped monster
                    {
                        AI.SelectCard(m);
                        return true;
                    }
                }
            }

            // 2. Disrupt Enemy monsters (flip dangerous attacker to defense):
            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            if (bestEnemy != null && bestEnemy.IsFaceup())
            {
                if (bestEnemy.Attack > 1100 || bestEnemy.Id == 77585513 || bestEnemy.Id == 23205979 || bestEnemy.Id == 71564252)
                {
                    AI.SelectCard(bestEnemy);
                    return true;
                }
            }

            // Otherwise, DO NOT summon Tsukuyomi!
            return false;
        }

        protected bool SmartMonsterSet()
        {
            // RULE 1: All Flip effect monsters MUST be set face-down!
            if (Card.HasType(CardType.Flip))
                return true;

            // RULE 2: Flip / Graveyard staple IDs
            if (Card.Id == 31560081 || Card.Id == 16226786 || Card.Id == 33508719 || Card.Id == 34124316)
                return true;

            // RULE 3: Wall monsters (Spirit Reaper, Marshmallon)
            if (Card.Id == 23205979 || Card.Id == 31305911)
                return true;

            // RULE 4: Defensive stats (DEF > ATK or 0 ATK)
            if (Card.Defense > Card.Attack || Card.Attack == 0)
                return true;

            // RULE 5: Facing stronger enemy monster
            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            if (bestEnemy != null && bestEnemy.IsFaceup() && Card.Attack < bestEnemy.Attack)
                return true;

            return false;
        }

        protected bool SmartMonsterSummon()
        {
            // Tsukuyomi has its own dedicated handler
            if (Card.Id == 34853266)
                return false;

            // NEVER normal summon Flip monsters face-up!
            if (Card.HasType(CardType.Flip))
                return false;
            if (Card.Id == 31560081 || Card.Id == 16226786 || Card.Id == 33508719 || Card.Id == 34124316)
                return false;

            // Never normal summon 0 ATK wall monsters
            if (Card.Attack == 0 || Card.Id == 23205979 || Card.Id == 31305911)
                return false;

            // OVEREXTENSION PREVENTION (일반소환권 아끼기)
            // If Bot already has dominant board presence and enemy has no monsters,
            // hold extra monsters in hand unless we have lethal!
            int botMonsters = Bot.GetMonsterCount();
            int enemyMonsters = Enemy.GetMonsterCount();
            ClientCard bestBot = Util.GetBestBotMonster();
            if (enemyMonsters == 0 && botMonsters >= 2 && bestBot != null && bestBot.Attack >= 2400)
            {
                int totalAtk = 0;
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && m.IsAttack()) totalAtk += m.Attack;
                }
                if (totalAtk < Enemy.LifePoints && totalAtk + Card.Attack < Enemy.LifePoints)
                {
                    return false; // Conserve resource in hand!
                }
            }

            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            if (bestEnemy != null && bestEnemy.IsFaceup() && Card.Attack < bestEnemy.Attack && Card.Defense >= Card.Attack)
                return false;

            return DefaultMonsterSummon();
        }

        protected bool SmartSpellSet()
        {
            // RULE 1: Never set Normal Spells (except bluff)
            if (Card.IsSpell() && !Card.HasType(CardType.QuickPlay))
            {
                // 1-SET HEAVY STORM BLUFF
                if (Card.Id == 19613556)
                {
                    if (Duel.Turn <= 2 && Bot.GetMonsterCount() == 0 && Bot.GetSpellCountWithoutField() == 0 && Enemy.GetSpellCount() <= 1)
                    {
                        if (_rand.Next(100) < 15)
                            return true; // 15% bluff set Heavy Storm!
                    }
                    return false;
                }
                return false;
            }

            // RULE 2: Prevent overset (Calculate FACEDOWN traps only, ignoring face-up Continuous cards!)
            int facedownSpells = 0;
            foreach (ClientCard s in Bot.GetSpells())
            {
                if (s != null && s.IsFacedown())
                    facedownSpells++;
            }

            // Allow up to 2 face-down cards safely, or 3 if protecting with Solemn Judgment
            if (facedownSpells >= 2)
            {
                bool hasSolemn = Bot.HasInSpellZone(41420027) || Card.Id == 41420027;
                if (!hasSolemn || facedownSpells >= 3)
                    return false;
            }

            return DefaultSpellSet();
        }

        protected bool SmartShrink()
        {
            // 0. 수축 카드 확인
            if (Card == null || Card.Id != 55713623) return false;

            // 1. [체인 및 아군 효과 차단]
            // 체인이 진행 중이거나 아군 효과에 체인하는 경우 발동 절대 금지! (자폭/팀킬 방지)
            if (Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
                return false;
            if (Duel.LastChainPlayer == 0)
                return false;

            // 2. [페이즈 엄격 제한 - 데미지 스텝 개시 시 / 데미지 계산 시 한정!]
            // 배틀페이즈 개시시(BattleStart)나 공격선언 전 조기 발동을 100% 원천 봉쇄!
            // 유희왕 정석: 상대가 메인2로 도망치거나 카운터치지 못하도록 오직 데미지 스텝에서만 사용!
            if (Duel.Phase != DuelPhase.Damage && Duel.Phase != DuelPhase.DamageCal)
                return false;

            // 3. 실제 교전 중인 몬스터 확인 (교전 몬스터가 없으면 절대 발동 안 함!)
            ClientCard enemyMonster = Enemy.BattlingMonster;
            ClientCard botMonster = Bot.BattlingMonster;

            if (enemyMonster == null || botMonster == null)
                return false;

            // 반드시 대상은 상대 앞면 공격 몬스터
            if (!IsEnemyCard(enemyMonster) || !enemyMonster.IsFaceup() || !enemyMonster.IsAttack())
                return false;

            // 아군 앞면 몬스터 검증
            if (!IsBotCard(botMonster) || !botMonster.IsFaceup())
                return false;

            // 4. 타점 역전 판정:
            // Case A: 아군 몬스터가 공격 표시인 경우
            if (botMonster.IsAttack())
            {
                // 현재 상태에서는 아군이 지거나 비기는데 (botMonster.Attack <= enemyMonster.Attack)
                // 적 공격력을 반토막 내면 아군이 승리할 수 있을 때! (enemyMonster.Attack / 2 < botMonster.Attack)
                if (botMonster.Attack <= enemyMonster.Attack && (enemyMonster.Attack / 2) < botMonster.Attack)
                {
                    AI.SelectCard(enemyMonster);
                    return true;
                }
            }
            // Case B: 아군 몬스터가 수비 표시인 경우 (상대가 아군 수비벽을 공격 중일 때)
            else if (botMonster.IsDefense())
            {
                // 상대 공격력이 아군 수비력보다 높은데 반토막 내면 아군 수비력 이하가 되어 아군이 생존할 때!
                if (enemyMonster.Attack > botMonster.Defense && (enemyMonster.Attack / 2) <= botMonster.Defense)
                {
                    AI.SelectCard(enemyMonster);
                    return true;
                }
            }

            return false;
        }

        protected bool SmartHeavyStorm()
        {
            int botSpells = Bot.GetSpellCountWithoutField();
            int enemySpells = Enemy.GetSpellCountWithoutField();
            if (enemySpells == 0) return false;
            return (enemySpells > botSpells) || (enemySpells >= 2 && botSpells <= 1);
        }

        protected bool SmartDarkHole()
        {
            int enemyCount = Enemy.GetMonsterCount();
            if (enemyCount == 0) return false;
            int botCount = Bot.GetMonsterCount();
            if (botCount == 0) return true;

            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            ClientCard bestBot = Util.GetBestBotMonster();
            if (bestEnemy != null && bestBot != null && bestEnemy.Attack > bestBot.Attack)
                return true;

            return enemyCount > botCount;
        }

        protected bool SmartMST()
        {
            // 체인에 이미 싸이크론이 있으면 중복 발동 차단!
            if (Duel.CurrentChain != null)
            {
                foreach (ClientCard c in Duel.CurrentChain)
                {
                    if (c != null && c.IsCode(5318639))
                        return false;
                }
            }

            // 상대 마법/함정이 0장이면 절대 발동 안 함!
            if (Enemy.GetSpellCount() == 0)
                return false;

            // 1순위: 상대 앞면 지속물 (Floodgate, Continuous, Field, Equip)
            foreach (ClientCard spell in Enemy.GetSpells())
            {
                if (spell != null && spell.IsFaceup() && 
                    (spell.HasType(CardType.Continuous) || spell.HasType(CardType.Field) || spell.HasType(CardType.Equip)))
                {
                    AI.SelectCard(spell);
                    return true;
                }
            }

            // 2순위: 상대 턴 엔드 페이즈 (안전한 세트 마함 저격)
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.End)
            {
                ClientCard target = Util.GetBestEnemySpell();
                if (target != null && IsEnemyCard(target))
                {
                    AI.SelectCard(target);
                    return true;
                }
            }

            // 3순위: 상대 마함 발동에 체인
            if (Duel.LastChainPlayer == 1 && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
            {
                ClientCard target = Util.GetBestEnemySpell();
                if (target != null && IsEnemyCard(target))
                {
                    AI.SelectCard(target);
                    return true;
                }
            }

            // 4순위: 아군 턴 배틀 전/후 세트 마함 청소
            if (Duel.Player == 0 && (Duel.Phase == DuelPhase.BattleStart || Duel.Phase == DuelPhase.Main2))
            {
                ClientCard target = Util.GetBestEnemySpell();
                if (target != null && IsEnemyCard(target))
                {
                    AI.SelectCard(target);
                    return true;
                }
            }

            return false;
        }

        protected bool SmartEnemyController()
        {
            // Mode 1: Tribute Frog/Token in Standby Phase to steal enemy boss
            if (Duel.Phase == DuelPhase.Standby && Enemy.GetMonsterCount() > 0)
            {
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && (m.Id == 12538374 || m.HasType(CardType.Token)))
                    {
                        ClientCard enemy = Util.GetBestEnemyMonster();
                        if (enemy != null && enemy.IsFaceup())
                        {
                            AI.SelectCard(m);
                            AI.SelectNextCard(enemy);
                            return true;
                        }
                    }
                }
            }

            // Mode 2: Shift enemy attacker to defense in Battle Phase
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle)
            {
                ClientCard attacker = Util.GetBestEnemyMonster();
                if (attacker != null && attacker.IsFaceup() && attacker.IsAttack() && attacker.Attack >= 1500)
                {
                    AI.SelectCard(attacker);
                    return true;
                }
            }

            return false;
        }

        protected bool SafeFallbackActivate()
        {
            if (Card == null) return false;

            // 1. NEVER blind-fire Quick-Play Spells! (Shrink, MST, Book of Moon, Enemy Controller, etc.)
            if (Card.IsSpell() && Card.HasType(CardType.QuickPlay))
                return false;

            // 2. NEVER blind-fire Normal Spells into an empty enemy field if they require targets
            if (Card.IsSpell())
            {
                if (Enemy.GetMonsterCount() == 0 && Enemy.GetSpellCount() == 0)
                    return false;
            }

            // 3. For Traps: never chain to own action and respond to opponent only
            if (Card.IsTrap())
            {
                // 카운터 함정은 전용 SmartSolemn 로직에서만 처리 (소환 무효 팀킬 방지)
                if (Card.HasType(CardType.Counter))
                    return false;
                if (Duel.Player == 0 && Duel.LastChainPlayer <= 0)
                    return false;
                if (Duel.LastChainPlayer == 0 && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
                    return false;
                return DefaultTrap();
            }

            return false;
        }

        protected bool SmartBreaker()
        {
            if (Enemy.GetSpellCount() == 0) return false;
            ClientCard target = Util.GetBestEnemySpell();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartGrandMoleSummon()
        {
            // 상대 필드에 몬스터가 1장 이상 있을 때 최우선 일반 소환!
            // 상대의 강력한 에이스 몬스터를 패/엑덱으로 바운스하고 그란몰도 패로 돌아오는 강력한 재활용 루프
            if (Enemy.GetMonsterCount() > 0)
                return true;
            return DefaultMonsterSummon();
        }

        protected bool SmartSnipeHunterSummon()
        {
            // 패에 버릴 카드가 2장 이상 있고 상대 필드에 파괴할 카드가 있을 때 소환!
            if (Bot.Hand.Count >= 2 && (Enemy.GetMonsterCount() > 0 || Enemy.GetSpellCount() > 0))
                return true;
            return DefaultMonsterSummon();
        }

        protected bool SmartSnipeHunterActivate()
        {
            // 패가 1장뿐이면 패 소모 방지를 위해 발동 중단
            if (Bot.Hand.Count <= 1) return false;

            // 상대 필드의 문제 카드 우선 파괴
            ClientCard target = Util.GetProblematicEnemyCard() ?? Util.GetBestEnemyCard();
            if (target != null)
            {
                // 버릴 카드 선택: 묘지 재활용 카드(황천개구리 등) 우선, 없으면 잉여 카드
                ClientCard discard = null;
                foreach (ClientCard h in Bot.Hand)
                {
                    if (h != null && h != Card)
                    {
                        if (h.Id == 12538374 || h.Id == 53839837)
                        {
                            discard = h;
                            break;
                        }
                        if (discard == null) discard = h;
                    }
                }

                if (discard != null)
                {
                    AI.SelectCard(discard);
                    AI.SelectNextCard(target);
                    return true;
                }
            }
            return false;
        }

        protected bool SmartEffectVeiler()
        {
            // 상대 메인 페이즈 한정
            if (Duel.Player != 1 || (Duel.Phase != DuelPhase.Main1 && Duel.Phase != DuelPhase.Main2))
                return false;

            // 상대 몬스터 효과 발동에 체인
            if (Duel.LastChainPlayer == 1 && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
            {
                ClientCard target = Util.GetLastChainCard();
                if (target != null && IsEnemyCard(target) && target.IsMonster() && !target.IsDisabled())
                {
                    AI.SelectCard(target);
                    return true;
                }
            }

            // 필드의 고위협 몬스터 사전 무효화
            ClientCard enemy = Util.GetProblematicEnemyMonster();
            if (enemy != null && enemy.IsFaceup() && !enemy.IsDisabled())
            {
                AI.SelectCard(enemy);
                return true;
            }

            return false;
        }

        protected bool SmartTragoediaActivate()
        {
            // 패에서 전투 데미지 발생 시 특수 소환
            if (Card.Location == CardLocation.Hand)
                return true;

            // 필드에서의 컨트롤 탈취 효과: 상대 고타점 몬스터와 동일한 레벨 몬스터를 버리고 영구 탈취!
            ClientCard enemy = Util.GetBestEnemyMonster();
            if (enemy != null && enemy.IsFaceup() && enemy.Attack >= 2000)
            {
                foreach (ClientCard h in Bot.Hand)
                {
                    if (h != null && h.IsMonster() && h.Level == enemy.Level)
                    {
                        AI.SelectOption(0);
                        AI.SelectCard(h);
                        AI.SelectNextCard(enemy);
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool SmartBLS()
        {
            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            if (bestEnemy == null) return false;

            if (bestEnemy.Attack >= 3000 || (bestEnemy.IsDefense() && bestEnemy.Defense >= 3000))
            {
                AI.SelectCard(bestEnemy);
                return true;
            }
            if (bestEnemy.IsMonsterInvincible() || bestEnemy.Id == 23205979 || bestEnemy.Id == 31305911)
            {
                AI.SelectCard(bestEnemy);
                return true;
            }
            if (Enemy.LifePoints <= 3000 && Enemy.GetMonsterCount() == 1 && bestEnemy.Attack < 3000)
                return false;

            if (bestEnemy.Attack < 3000 && !bestEnemy.IsDefense())
                return false;

            AI.SelectCard(bestEnemy);
            return true;
        }

        protected bool SmartChaosSorcerer()
        {
            foreach (ClientCard card in Enemy.GetMonsters())
            {
                if (card != null && card.IsFaceup())
                {
                    if (card.Attack >= 2300 || card.IsMonsterDangerous() || card.IsMonsterInvincible())
                    {
                        AI.SelectCard(card);
                        return true;
                    }
                }
            }
            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            if (bestEnemy != null && bestEnemy.IsFaceup() && (bestEnemy.Attack >= 2300 || bestEnemy.Defense >= 2300))
            {
                AI.SelectCard(bestEnemy);
                return true;
            }
            if (Enemy.GetMonsterCount() > 0)
            {
                ClientCard target = Util.GetProblematicEnemyMonster() ?? Util.GetBestEnemyMonster();
                if (target != null && target.IsFaceup())
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }

        protected bool SmartDarkArmedDragon()
        {
            ClientCard target = Util.GetProblematicEnemyCard() ?? Util.GetBestEnemyCard();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartRaiza()
        {
            ClientCard target = Util.GetBestEnemyMonster() ?? Util.GetBestEnemySpell();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartCaius()
        {
            ClientCard target = Util.GetProblematicEnemyCard() ?? Util.GetBestEnemyCard();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartMobius()
        {
            if (Enemy.GetSpellCount() == 0) return false;
            return true;
        }

        protected bool SmartZaborg()
        {
            ClientCard target = Util.GetProblematicEnemyMonster() ?? Util.GetBestEnemyMonster();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartScapegoat()
        {
            if (Duel.Player == 0) return false;
            if (Duel.Phase == DuelPhase.BattleStart || Duel.Phase == DuelPhase.Battle || Duel.Phase == DuelPhase.End)
                return true;
            return false;
        }

        protected bool SmartBookOfMoon()
        {
            // Scenario 1: Flip recycling combo (Magician of Faith, Night Assailant, TER)
            if (Duel.Player == 0 && Duel.Phase == DuelPhase.Main1)
            {
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && m.IsFaceup())
                    {
                        if (m.Id == 31560081 || m.Id == 16226786) // Faith or Night Assailant
                        {
                            AI.SelectCard(m);
                            return true;
                        }
                        if (m.Id == 63519819 && m.EquipCards.Count > 0) // TER
                        {
                            AI.SelectCard(m);
                            return true;
                        }
                    }
                }
            }

            // Scenario 2: Disrupt enemy attacker during enemy Battle Phase
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle)
            {
                ClientCard attacker = Util.GetBestEnemyMonster();
                if (attacker != null && attacker.IsFaceup() && attacker.IsAttack() && attacker.Attack >= 1200)
                {
                    AI.SelectCard(attacker);
                    return true;
                }
            }

            // Scenario 3: Bot's Turn - Flip down strong enemy monster to destroy by battle
            if (Duel.Player == 0 && Duel.Phase == DuelPhase.Main1)
            {
                ClientCard bestEnemy = Util.GetBestEnemyMonster();
                ClientCard bestBot = Util.GetBestBotMonster();
                if (bestEnemy != null && bestEnemy.IsFaceup() && bestBot != null)
                {
                    if (bestEnemy.Attack > bestBot.Attack && bestEnemy.Defense < bestBot.Attack)
                    {
                        AI.SelectCard(bestEnemy);
                        return true;
                    }
                }
            }

            // Otherwise, do not activate blindly!
            return false;
        }

        protected bool SmartMirrorForce()
        {
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle)
                return true;
            return false;
        }

        protected bool SmartSakuretsuArmor()
        {
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle)
                return true;
            return false;
        }

        protected bool SmartDimensionalPrison()
        {
            if (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle)
                return true;
            return false;
        }

        protected bool SmartRingOfDestruction()
        {
            ClientCard target = Util.GetBestEnemyMonster();
            if (target != null && target.IsFaceup() && target.Attack > 0)
            {
                // Lethal strike: Win the duel immediately if enemy LP <= target ATK!
                if (Enemy.LifePoints <= target.Attack && Bot.LifePoints > target.Attack)
                {
                    AI.SelectCard(target);
                    return true;
                }

                // Suicide protection: NEVER destroy self!
                if (Bot.LifePoints <= target.Attack)
                    return false;

                // Targeted removal of high-threat monsters (ATK >= 1800) or battle defense
                if (target.Attack >= 1800 || (Duel.Player == 1 && Duel.Phase == DuelPhase.Battle))
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }

        protected bool SmartTorrentialTribute()
        {
            // Board wipe intelligence: Protect Bot's own dominant board!
            int botCount = Bot.GetMonsterCount();
            int enemyCount = Enemy.GetMonsterCount();

            ClientCard bestBot = Util.GetBestBotMonster();
            ClientCard bestEnemy = Util.GetBestEnemyMonster();

            if (botCount >= 2 && bestBot != null && bestBot.Attack >= 2000)
            {
                if (bestEnemy != null && bestEnemy.Attack < 2000 && enemyCount == 1)
                    return false; // Do not destroy own dominant board for a single weak monster!
            }

            // Wipe if enemy has numerical advantage or bot has no monsters
            if (enemyCount > botCount || botCount == 0)
                return true;

            // Wipe if enemy summoned a major boss (ATK >= 2400)
            if (bestEnemy != null && bestEnemy.Attack >= 2400)
                return true;

            return false;
        }

        protected bool SmartSolemnJudgment()
        {
            // [기존 WindBot 핵심 안전망 복제 1] 신심 자신이 대상일 때 회피
            if (Util.IsChainTargetOnly(Card)) return false;

            // [기존 WindBot 핵심 안전망 복제 2 - 아군 소환 팀킬 원천 차단!]
            // 아군 턴(Duel.Player == 0)이고 체인이 없을 때(Duel.LastChainPlayer == -1)는
            // 아군 몬스터의 일반 소환 / 특수 소환 타이밍이므로 100% 무조건 발동 금지!
            if (Duel.Player == 0 && Duel.LastChainPlayer == -1)
                return false;

            // [안전망 3] 아군 효과나 체인에 절대 발동 금지
            if (Duel.LastChainPlayer == 0) return false;

            // [안전망 4] LP 임계값 (1000 이하에서는 안전을 위해 보류)
            if (Bot.LifePoints <= 1000) return false;

            // =====================================================================
            // CASE A: 마법 / 함정 발동 무효화 타이밍 (Duel.LastChainPlayer == 1)
            // =====================================================================
            if (Duel.LastChainPlayer == 1 && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
            {
                ClientCard chainCard = Util.GetLastChainCard();
                if (chainCard == null || !IsEnemyCard(chainCard)) return false;

                // 1순위: 보드 클리어 및 승부처 파워 마법/함정 (무조건 카운터)
                int[] highThreatSpells = new int[] {
                    18144506, // Harpie's Feather Duster
                    19613556, // Heavy Storm
                    53129443, // Dark Hole
                    12580477, // Raigeki
                    42703248, // Giant Trunade
                    55144522, // Pot of Greed
                    79571449, // Graceful Charity
                    44763025, // Delinquent Duo
                    45986603, // Snatch Steal
                    87953373, // Brain Control
                    83764718, // Monster Reborn
                    70828912, // Premature Burial
                    46411259, // Metamorphosis
                    45906428, // Miracle Fusion
                    82732705, // Skill Drain
                    51452091, // Royal Decree
                    57953380, // Card of Safe Return
                    86780027, // Solidarity
                    63995093, // Machine Duplication
                    53582587, // Torrential Tribute
                    44095762  // Mirror Force
                };

                foreach (int threatId in highThreatSpells)
                {
                    if (chainCard.Id == threatId) return true;
                }

                // 2순위: 아군 필드가 이미 구축되어 있을 때 상대의 제거 마법 차단
                if (chainCard.IsSpell() && (Bot.GetMonsterCount() >= 2 || Bot.GetSpellCountWithoutField() >= 2))
                    return true;

                // 3순위: 기타 상대의 위협적인 마법/함정 카드
                return DefaultTrap();
            }

            // =====================================================================
            // CASE B: 몬스터 소환 무효화 타이밍 (체인이 없을 때: Duel.LastChainPlayer == -1)
            // =====================================================================
            if (Duel.LastChainPlayer == -1)
            {
                // [기존 WindBot 핵심 안전망 복제 3] 소환 주체가 적(Duel.LastSummonPlayer == 1)이어야만 함!
                if (Duel.LastSummonPlayer != 1) return false;

                // 소환 중인 몬스터가 아군 몬스터라면 100% 차단!
                if (Duel.SummoningCards != null && Duel.SummoningCards.Count > 0)
                {
                    foreach (ClientCard m in Duel.SummoningCards)
                    {
                        if (m != null && IsBotCard(m)) return false;
                    }
                }

                // 상대 소환 몬스터 검증
                ClientCard summonTarget = null;
                if (Duel.SummoningCards != null && Duel.SummoningCards.Count > 0)
                {
                    summonTarget = Duel.SummoningCards[0];
                }
                if (summonTarget == null)
                    summonTarget = Util.GetBestEnemyMonster();

                if (summonTarget != null && IsEnemyCard(summonTarget))
                {
                    // 최고 위협 보스 몬스터 (반드시 무효)
                    int[] highThreatMonsters = new int[] {
                        504700118, 72989439, // BLS Envoy
                        9596126,             // Chaos Sorcerer
                        65192027,            // DAD
                        70095154,            // Cyber Dragon
                        5556499,             // Machina Fortress
                        9748752, 49721904,   // Caius
                        73125233, 44155002,  // Raiza
                        4929256,             // Mobius
                        51945556,            // Zaborg
                        77585513,            // Jinzo
                        52687916,            // Trishula
                        44508094,            // Stardust
                        73580471,            // Black Rose
                        78651105,            // Barbaros
                        22061412             // The Shining
                    };

                    foreach (int bossId in highThreatMonsters)
                    {
                        if (summonTarget.Id == bossId) return true;
                    }

                    // 아군 필드를 압도하는 2100+ 타점 비터 소환 무효
                    if (summonTarget.Attack >= 2100 || Util.GetOneEnemyBetterThanMyBest() != null)
                        return true;

                    // 특수 소환된 몬스터(싱크로, 융합, 카오스 등)는 무조건 무효!
                    if (summonTarget.IsSpecialSummoned)
                        return true;
                }

                // 하급 일반 소환에는 LP 50% 낭비 방지!
                return false;
            }

            return false;
        }

        protected bool SmartSolemnWarning()
        {
            // [기존 WindBot 핵심 안전망 복제 1] 경고 자신이 대상일 때 회피
            if (Util.IsChainTargetOnly(Card)) return false;

            // [기존 WindBot 핵심 안전망 복제 2 - 아군 소환 팀킬 원천 차단!]
            if (Duel.Player == 0 && Duel.LastChainPlayer == -1)
                return false;

            // [안전망 3] 아군 효과나 체인에 절대 발동 금지
            if (Duel.LastChainPlayer == 0) return false;

            // [안전망 4] LP 2000 초과 필수
            if (Bot.LifePoints <= 2000) return false;

            // 체인 중일 때: 특수 소환 마법/함정/몬스터 효과 무효
            if (Duel.LastChainPlayer == 1 && Duel.CurrentChain != null && Duel.CurrentChain.Count > 0)
            {
                ClientCard chainCard = Util.GetLastChainCard();
                if (chainCard != null && IsEnemyCard(chainCard))
                {
                    int[] ssSpells = new int[] {
                        83764718, // Monster Reborn
                        70828912, // Premature Burial
                        45906428, // Miracle Fusion
                        97077563, // Call of the Haunted
                        46411259, // Metamorphosis
                        63995093  // Machine Duplication
                    };
                    foreach (int id in ssSpells)
                    {
                        if (chainCard.Id == id) return true;
                    }
                }
                return DefaultTrap();
            }

            // 소환 무효일 때: 상대 소환만 무효
            if (Duel.LastChainPlayer == -1 && Duel.LastSummonPlayer == 1)
            {
                if (Duel.SummoningCards != null && Duel.SummoningCards.Count > 0)
                {
                    foreach (ClientCard m in Duel.SummoningCards)
                    {
                        if (m != null && IsBotCard(m)) return false;
                    }
                }

                ClientCard bestEnemy = Util.GetBestEnemyMonster();
                if (bestEnemy != null && IsEnemyCard(bestEnemy))
                {
                    if (bestEnemy.Attack >= 1900 || Util.GetOneEnemyBetterThanMyBest() != null || bestEnemy.IsSpecialSummoned)
                        return true;

                    int[] warningBosses = new int[] {
                        70095154, 5556499, 42940404, 40044918, 71564252,
                        9748752, 73125233, 77585513, 52687916, 44508094
                    };
                    foreach (int id in warningBosses)
                    {
                        if (bestEnemy.Id == id) return true;
                    }
                }
                return false;
            }

            return false;
        }

        protected bool SmartSnatchSteal()
        {
            ClientCard target = Util.GetBestEnemyMonster();
            if (target != null && target.Attack >= 1500)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartBrainControl()
        {
            if (Bot.LifePoints <= 1000) return false;
            ClientCard target = Util.GetBestEnemyMonster();
            if (target != null && target.Attack >= 1500)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartSmashingGround()
        {
            return Enemy.GetMonsterCount() > 0;
        }

        protected bool SmartHammerShot()
        {
            ClientCard bestEnemy = Util.GetBestEnemyMonster();
            ClientCard bestBot = Util.GetBestBotMonster();
            if (bestEnemy == null) return false;
            if (bestBot == null) return true;
            return bestEnemy.Attack >= bestBot.Attack;
        }

        protected bool SmartFissure()
        {
            return Enemy.GetMonsterCount() > 0;
        }

        protected bool SmartNoblemanOfCrossout()
        {
            foreach (ClientCard m in Enemy.GetMonsters())
            {
                if (m != null && m.IsFacedown())
                {
                    AI.SelectCard(m);
                    return true;
                }
            }
            return false;
        }

        protected bool SmartMonsterReborn()
        {
            ClientCard target = Util.GetBestBotMonster() ?? Util.GetBestEnemyMonster();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return true;
        }

        protected bool SmartPrematureBurial()
        {
            if (Bot.LifePoints <= 800) return false;
            return SmartMonsterReborn();
        }

        protected bool SmartCallOfTheHaunted()
        {
            return DefaultCallOfTheHaunted();
        }

        protected bool SmartBookOfLife()
        {
            return Enemy.Graveyard.Count > 0;
        }

        protected bool SmartTreebornFrog()
        {
            if (Bot.GetSpellCount() == 0)
            {
                AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        protected bool SmartStratos()
        {
            if (Enemy.GetSpellCount() > 0)
                AI.SelectOption(1);
            else
                AI.SelectOption(0);
            return true;
        }

        protected bool SmartGeminiSpark()
        {
            ClientCard target = Util.GetProblematicEnemyCard() ?? Util.GetBestEnemyCard();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        protected bool SmartMetamorphosis()
        {
            foreach (ClientCard m in Bot.GetMonsters())
            {
                if (m != null)
                {
                    if (m.Level == 1)
                    {
                        AI.SelectCard(m);
                        return true;
                    }
                    if ((m.Level == 5 || m.Level == 6) && (m.Id == 9596126 || m.Attacked))
                    {
                        AI.SelectCard(m);
                        return true;
                    }
                }
            }
            return false;
        }

        protected bool SmartTER()
        {
            if (Card.EquipCards.Count == 0 && Enemy.GetMonsterCount() > 0)
            {
                ClientCard target = Util.GetProblematicEnemyMonster() ?? Util.GetBestEnemyMonster();
                if (target != null)
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }

        protected bool SmartMagicianOfFaith()
        {
            int[] prioritySpells = new int[] {
                55144522, 79571449, 44763025, 45986603, 83764718, 70828912,
                19613556, 53129443, 12580477, 71044499, 5318639, 14087893, 73915051
            };
            foreach (int spellId in prioritySpells)
            {
                foreach (ClientCard c in Bot.Graveyard)
                {
                    if (c != null && c.Id == spellId)
                    {
                        AI.SelectCard(c);
                        return true;
                    }
                }
            }
            return true;
        }

        protected bool SmartNightAssailant()
        {
            if (Enemy.GetMonsterCount() > 0)
            {
                ClientCard target = Util.GetProblematicEnemyMonster() ?? Util.GetBestEnemyMonster();
                if (target != null)
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            foreach (ClientCard c in Bot.Graveyard)
            {
                if (c != null && c.HasType(CardType.Flip) && c != Card)
                {
                    AI.SelectCard(c);
                    return true;
                }
            }
            return true;
        }

        protected bool SmartDelinquentDuo()
        {
            return Bot.LifePoints > 1500 && Enemy.Hand.Count >= 2;
        }

        protected bool SmartConfiscation()
        {
            return Bot.LifePoints > 1500 && Enemy.Hand.Count >= 1;
        }

        protected bool SmartPotOfDuality()
        {
            if (Enemy.GetMonsterCount() > 0 && Bot.GetMonsterCount() == 0)
            {
                foreach (ClientCard c in Bot.Hand)
                {
                    if (c != null && (c.Id == 70095154 || c.Id == 83104731))
                        return false; // Prioritize Cyber Dragon Special Summon!
                }
            }
            return true;
        }

        protected bool SmartMonsterRepos()
        {
            if (Card.IsAttack() && (Card.Attack == 0 || Card.Defense > Card.Attack))
                return true;
            return DefaultMonsterRepos();
        }
    }

    [Deck("GoatUniversal", "AI_GoatUniversal", "Normal")]
    public class GoatUniversalExecutor : GateballBaseExecutor
    {
        public GoatUniversalExecutor(GameAI ai, Duel duel) : base(ai, duel) {}
    }

    [Deck("0703Universal", "AI_0703Universal", "Normal")]
    public class S0703UniversalExecutor : GateballBaseExecutor
    {
        public S0703UniversalExecutor(GameAI ai, Duel duel) : base(ai, duel) {}
    }

    [Deck("1103Universal", "AI_1103Universal", "Normal")]
    public class S1103UniversalExecutor : GateballBaseExecutor
    {
        public S1103UniversalExecutor(GameAI ai, Duel duel) : base(ai, duel) {}
    }

    [Deck("GoatDaemon", "AI_GoatDaemon", "Normal")]
    public class GoatDaemonExecutor : GateballBaseExecutor
    {
        public GoatDaemonExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 94585852, ActivatePandemonium); // Pandemonium
            AddExecutor(ExecutorType.Activate, 51452091);                      // Royal Decree
            AddExecutor(ExecutorType.Summon, 35975813, SummonTerrorking);      // Terrorking Archfiend
            AddExecutor(ExecutorType.Summon, 9603356);                         // Shadowknight Archfiend
        }

        private bool ActivatePandemonium()
        {
            foreach (ClientCard s in Bot.GetSpells())
            {
                if (s != null && s.IsFaceup() && s.Id == 94585852) return false;
            }
            return true;
        }

        private bool SummonTerrorking()
        {
            foreach (ClientCard m in Bot.GetMonsters())
            {
                if (m != null && m.IsFaceup() && (m.Id == 9603356 || m.Id == 72192100))
                    return true;
            }
            return Bot.GetMonsterCount() == 0;
        }
    }

    [Deck("GoatMetamorphosisChaos", "AI_GoatMetamorphosisChaos", "Normal")]
    public class GoatMetamorphosisChaosExecutor : GateballBaseExecutor
    {
        public GoatMetamorphosisChaosExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 504700054);                     // Thunder Dragon discard
            AddExecutor(ExecutorType.Activate, 46411259, SmartMetamorphosis);  // Metamorphosis combo
            AddExecutor(ExecutorType.Activate, 73915051, SmartScapegoat);      // Scapegoat
        }
    }

    [Deck("GoatZorcChaosTurbo", "AI_GoatZorcChaosTurbo", "Normal")]
    public class GoatZorcChaosTurboExecutor : GateballBaseExecutor
    {
        public GoatZorcChaosTurboExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 504700054);                     // Thunder Dragon
            AddExecutor(ExecutorType.Summon, 504700169);                       // Manju
            AddExecutor(ExecutorType.Summon, 504700040);                       // Senju
            AddExecutor(ExecutorType.Activate, 96420087);                      // Contract with the Dark Master
            AddExecutor(ExecutorType.Activate, 97642679, ActivateZorc);        // Zorc Dice
            AddExecutor(ExecutorType.Activate, 33184167, ActivateTribe);       // Tribe-Infecting Virus
        }

        private bool ActivateZorc()
        {
            return Enemy.GetMonsterCount() > 0 || Enemy.LifePoints <= 2000;
        }

        private bool ActivateTribe()
        {
            return Enemy.GetMonsterCount() > 0 && Bot.Hand.Count > 0;
        }
    }

    [Deck("GoatChaosWarrior", "AI_GoatChaosWarrior", "Normal")]
    public class GoatChaosWarriorExecutor : GateballBaseExecutor
    {
        public GoatChaosWarriorExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 504700054);                     // Thunder Dragon
            AddExecutor(ExecutorType.Activate, 504700055);                     // ROTA (GOAT)
            AddExecutor(ExecutorType.Summon, 39507162);                        // Blade Knight
            AddExecutor(ExecutorType.Summon, 7572887);                         // D.D. Warrior Lady
            AddExecutor(ExecutorType.Summon, 74131780);                        // Exiled Force
        }
    }

    [Deck("GoatHinoKaguTsuchi", "AI_GoatHinoKaguTsuchi", "Normal")]
    public class GoatHinoKaguTsuchiExecutor : GateballBaseExecutor
    {
        public GoatHinoKaguTsuchiExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 42703248, ActivateTrunade);     // Giant Trunade
            AddExecutor(ExecutorType.Summon, 41089128);                        // Flame Ruler
            AddExecutor(ExecutorType.Summon, 504700131, SummonHino);           // Hino-Kagu-Tsuchi
        }

        private bool ActivateTrunade()
        {
            return Enemy.GetSpellCount() >= 2 || (Bot.HasInHand(504700131) && Enemy.GetSpellCount() >= 1);
        }

        private bool SummonHino()
        {
            if (Bot.GetMonsterCount() >= 2) return true;
            foreach (ClientCard m in Bot.GetMonsters())
            {
                if (m != null && m.IsFaceup() && m.Id == 41089128) return true;
            }
            return false;
        }
    }

    [Deck("0703SafeReturnBeast", "AI_0703SafeReturnBeast", "Normal")]
    public class S0703SafeReturnBeastExecutor : GateballBaseExecutor
    {
        public S0703SafeReturnBeastExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 57953380);                      // Card of Safe Return (MAX PRIORITY)
            AddExecutor(ExecutorType.Summon, 7093411);                         // Sapphire Pegasus
            AddExecutor(ExecutorType.Activate, 7093411);
            AddExecutor(ExecutorType.Activate, 60876124);                      // Rare Value
            AddExecutor(ExecutorType.Activate, 81439173);                      // Foolish Burial
            AddExecutor(ExecutorType.Activate, 46668237);                      // Green Baboon
            AddExecutor(ExecutorType.Activate, 77121851);                      // Manticore of Darkness
        }
    }

    [Deck("0703Undead", "AI_0703Undead", "Normal")]
    public class S0703UndeadExecutor : GateballBaseExecutor
    {
        public S0703UndeadExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 57953380);                      // Card of Safe Return
            AddExecutor(ExecutorType.Activate, 63995093, ActivateMachineDup);  // Machine Duplication
            AddExecutor(ExecutorType.Activate, 85087012);                      // Card Trooper
            AddExecutor(ExecutorType.Activate, 17259470, ActivateZombieMaster);// Zombie Master
            AddExecutor(ExecutorType.Activate, 2204140, ActivateBookOfLife);   // Book of Life
            AddExecutor(ExecutorType.Summon, 77044671);                        // Pyramid Turtle
        }

        private bool ActivateMachineDup()
        {
            foreach (ClientCard m in Bot.GetMonsters())
            {
                if (m != null && m.IsFaceup() && m.Id == 85087012 && m.Attack <= 500)
                {
                    AI.SelectCard(m);
                    return true;
                }
            }
            return false;
        }

        private bool ActivateZombieMaster()
        {
            return Bot.Graveyard.Count > 0 && Bot.Hand.Count > 0;
        }

        private bool ActivateBookOfLife()
        {
            return Enemy.Graveyard.Count > 0 && Bot.Graveyard.Count > 0;
        }
    }

    [Deck("0703OceanBeat", "AI_0703OceanBeat", "Normal")]
    public class S0703OceanBeatExecutor : GateballBaseExecutor
    {
        public S0703OceanBeatExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Summon, 80344569, SmartGrandMoleSummon);  // Grand Mole (High priority bounce)
            AddExecutor(ExecutorType.Activate, 80344569);                      // Grand Mole bounce
            AddExecutor(ExecutorType.Summon, 40044918);                        // Stratos
            AddExecutor(ExecutorType.Activate, 40044918);
            AddExecutor(ExecutorType.Summon, 37195861);                        // Ocean
            AddExecutor(ExecutorType.Activate, 37195861);                      // Ocean Recycle
            AddExecutor(ExecutorType.Activate, 71587526, DefaultTrap);         // Pulling the Rug (Negate Gadget/Monarch/Stratos summon)
            AddExecutor(ExecutorType.Activate, 55713623, SmartShrink);         // Shrink Smart Combat Trick (Damage Step only)
            AddExecutor(ExecutorType.Activate, 55773067, ActivateDropOff);     // Drop Off
        }

        private bool ActivateDropOff()
        {
            return Duel.Player == 1 && Duel.Phase == DuelPhase.Draw;
        }
    }

    [Deck("1103SolidarityGadget", "AI_1103SolidarityGadget", "Normal")]
    public class S1103SolidarityGadgetExecutor : GateballBaseExecutor
    {
        public S1103SolidarityGadgetExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 86780027);                      // Solidarity (ATK +800)
            AddExecutor(ExecutorType.Summon, 42940404);                        // Machina Gearframe
            AddExecutor(ExecutorType.SpSummon, 5556499);                       // Machina Fortress
            AddExecutor(ExecutorType.Summon, 41172955);                        // Green Gadget
            AddExecutor(ExecutorType.Summon, 86445415);                        // Red Gadget
            AddExecutor(ExecutorType.Summon, 13839120);                        // Yellow Gadget
            AddExecutor(ExecutorType.Activate, 41172955);
            AddExecutor(ExecutorType.Activate, 86445415);
            AddExecutor(ExecutorType.Activate, 13839120);
        }
    }

    [Deck("1103SkillDrainHero", "AI_1103SkillDrainHero", "Normal")]
    public class S1103SkillDrainHeroExecutor : GateballBaseExecutor
    {
        public S1103SkillDrainHeroExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 82732705);                      // Skill Drain
            AddExecutor(ExecutorType.Summon, 78651105);                        // Barbaros (3000 ATK under Drain)
            AddExecutor(ExecutorType.Summon, 69884162);                        // Neos Alius
            AddExecutor(ExecutorType.Summon, 71564252);                        // Thunder King Rai-Oh
            AddExecutor(ExecutorType.Activate, 33846209, ActivateGeminiSpark); // Gemini Spark Sacrifice Escape
            AddExecutor(ExecutorType.Activate, 37412656);                      // Hero Blast
            AddExecutor(ExecutorType.Activate, 45906428);                      // Miracle Fusion -> The Shining
        }

        private bool ActivateGeminiSpark()
        {
            if (Enemy.GetMonsterCount() > 0 || Enemy.GetSpellCount() > 0)
            {
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && m.IsFaceup() && m.Id == 69884162)
                    {
                        ClientCard target = Util.GetProblematicEnemyCard() ?? Util.GetBestEnemyCard();
                        if (target != null)
                        {
                            AI.SelectCard(target);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    [Deck("1103Monarch", "AI_1103Monarch", "Normal")]
    public class S1103MonarchExecutor : GateballBaseExecutor
    {
        public S1103MonarchExecutor(GameAI ai, Duel duel) : base(ai, duel) {}

        protected override void InitArchetypeTactics()
        {
            AddExecutor(ExecutorType.Activate, 12538374, ActivateTreeborn);    // Treeborn Frog Revive
            AddExecutor(ExecutorType.Summon, 4904812);                         // Genex Undine (dumps Frog)
            AddExecutor(ExecutorType.Activate, 98045062, ActivateEnemyController); // Enemy Controller Take Control
            AddExecutor(ExecutorType.Activate, 6540606, ActivateAoi);          // Spiritual Water Art - Aoi
            AddExecutor(ExecutorType.Summon, 9748752);                         // Caius
            AddExecutor(ExecutorType.Summon, 73125233);                        // Raiza
        }

        private bool ActivateTreeborn()
        {
            return Bot.GetSpellCount() == 0;
        }

        private bool ActivateEnemyController()
        {
            if (Duel.Phase == DuelPhase.Standby && Enemy.GetMonsterCount() > 0)
            {
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && m.Id == 12538374)
                    {
                        ClientCard enemy = Util.GetBestEnemyMonster();
                        if (enemy != null)
                        {
                            AI.SelectCard(m);
                            AI.SelectNextCard(enemy);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ActivateAoi()
        {
            if (Enemy.Hand.Count > 0)
            {
                foreach (ClientCard m in Bot.GetMonsters())
                {
                    if (m != null && (m.Id == 12538374 || m.Id == 9126351))
                    {
                        AI.SelectCard(m);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

