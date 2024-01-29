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
        }

        private IEnumerator DisableNonSelectedBallers()
        {
            yield return new WaitForSeconds(0.5f);
            foreach (BallerSelector baller in _ballersSelector)
            {
                baller.gameObject.SetActive(baller.ID != _selectedBallerId);
            }
        }

        private void OnBallSelected(int selectedBallId)
        {
            _selectedBallId = selectedBallId;
            StartCoroutine(DisableNonSelectedBalls());
        }

        private IEnumerator DisableNonSelectedBalls()
        {
            yield return new WaitForSeconds(0.5f);
            foreach (BallSelector ball in _ballsSelector)
            {
                ball.gameObject.SetActive(ball.ID != _selectedBallId);
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