using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    public class SelectionMenuView : View
    {
        [SerializeField]
        private Button _start;
        [SerializeField]
        private Transform _ballersSelectorRoot;
        [SerializeField]
        private Transform _ballsSelectorRoot;
        [SerializeField]
        private MatchConfiguration _matchConfiguration;

        private int _selectedBallerId = -1;
        private int _selectedBallId = -1;

        private List<BallerSelector> _ballersSelector = new List<BallerSelector>();
        private List<BallSelector> _ballsSelector = new List<BallSelector>();

        private void Awake()
        {
            _start.onClick.AddListener(OnStartClick);
        }

        private void Start()
        {
            FillCharactersSelector(_matchConfiguration.CreateBallers());
            FillBallsSelector(_matchConfiguration.CreateBalls());
            EvaluateGameButtonVisibility();
        }

        private void FillCharactersSelector(List<BallerSelector> ballersSelectorData)
        {
            _ballersSelector = ballersSelectorData;

            foreach (BallerSelector baller in _ballersSelector)
            {
                baller.transform.SetParent(_ballersSelectorRoot);
                baller.SelectedAction += OnBallerSelected;
            }
        }

        private void FillBallsSelector(List<BallSelector> ballsData)
        {
            _ballsSelector = ballsData;

            foreach (BallSelector ball in _ballsSelector)
            {
                ball.transform.SetParent(_ballsSelectorRoot);
                ball.SelectedAction += OnBallSelected;
            }
        }

        private void OnBallerSelected(int selectedBallerId)
        {
            _selectedBallerId = selectedBallerId;
            StartCoroutine(DisableNonSelectedBallers());
            EvaluateGameButtonVisibility();
        }

        private IEnumerator DisableNonSelectedBallers()
        {
            foreach (BallerSelector baller in _ballersSelector)
            {
                if (baller.ID != _selectedBallerId)
                {
                    baller.DisableInteraction();
                }
            }

            yield return new WaitForSeconds(0.5f);

            foreach (BallerSelector baller in _ballersSelector)
            {
                baller.gameObject.SetActive(baller.ID == _selectedBallerId);
            }
        }

        private void OnBallSelected(int selectedBallId)
        {
            _selectedBallId = selectedBallId;
            StartCoroutine(DisableNonSelectedBalls());
            EvaluateGameButtonVisibility();
        }

        private IEnumerator DisableNonSelectedBalls()
        {
            foreach (BallSelector ball in _ballsSelector)
            {
                if (ball.ID != _selectedBallId)
                {
                    ball.DisableInteraction();
                }
            }

            yield return new WaitForSeconds(0.5f);

            foreach (BallSelector ball in _ballsSelector)
            {
                ball.gameObject.SetActive(ball.ID == _selectedBallId);
            }
        }

        private void EvaluateGameButtonVisibility()
        {
            if (_selectedBallerId != -1 && _selectedBallId != -1)
            {
                _start.gameObject.SetActive(true);
            }
            else
            {
                _start.gameObject.SetActive(false);
            }
        }

        private void OnStartClick()
        {
            MatchConfigurationData selectedData = new MatchConfigurationData(_selectedBallerId, _selectedBallId);
            InvokeAction<StoreMatchConfigurationAction, MatchConfigurationData>(selectedData);
        }

        private void OnDestroy()
        {
            foreach (BallerSelector baller in _ballersSelector)
            {
                baller.SelectedAction -= OnBallerSelected;
            }

            foreach (BallSelector ball in _ballsSelector)
            {
                ball.SelectedAction -= OnBallSelected;
            }
        }
    }
}