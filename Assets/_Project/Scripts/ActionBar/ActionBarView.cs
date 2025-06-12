using System;
using _Project.Scripts.Figure;
using _Project.Scripts.Providers;
using _Project.Scripts.Providers.FigureField;
using _Project.Scripts.Services.Raycast;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ActionBar
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _slots;

        private ActionBarModel _actionBarModel;

        private IRaycastService _raycastService;

        private RefreshButtonUIView _refreshButton;

        public event Action Defeated;
        public event Action FigureCountChanged;

        public void Initialize(IFigureFieldProvider figureFieldProvider, IRaycastService raycastService,
            RefreshButtonUIView refreshButton, int matchesCount)
        {
            _actionBarModel = new ActionBarModel(figureFieldProvider, matchesCount, _slots.Length);

            _raycastService = raycastService;

            _refreshButton = refreshButton;
            
            _actionBarModel.FigureRemoved += ShowPlug;
            _refreshButton.Refreshed += OnRefresh;
        }

        private void OnDisable()
        {
            _actionBarModel.FigureRemoved -= ShowPlug;
            _refreshButton.Refreshed -= OnRefresh;
        }

        private void Update()
        {
            if (_raycastService.TryGetComponent<FigureView>(out var figureView))
                SetFigure(figureView);
        }

        private void OnRefresh()
        {
            _actionBarModel.RefreshList();
            ShowAllPlugs();
        }

    private void SetFigure(FigureView figureView)
        {
            var freeSlot = GetFreeSlot();

            if (freeSlot == null)
                return;

            SetFigureParams(figureView, freeSlot);

            _actionBarModel.AddFigure(figureView);

            if (_actionBarModel.IsBarFull())
                Defeated?.Invoke();

            else FigureCountChanged?.Invoke();
        }

        private Transform GetFreeSlot()
        {
            foreach (var slot in _slots)
            {
                var slotTransform = slot.transform;

                if (slotTransform.childCount == 0)
                {
                    slot.enabled = false;
                    return slotTransform;
                }
            }

            return null;
        }

        private void SetFigureParams(FigureView figureView, Transform slot)
        {
            figureView.transform.localPosition = slot.transform.position;
            figureView.transform.localRotation = Quaternion.identity;
            figureView.transform.SetParent(slot.transform);

            figureView.DisablePhysics();
        }

        private void ShowPlug(FigureView figureView)
        {
            foreach (var slot in _slots)
            {
                if (slot.transform != figureView.transform.parent)
                    continue;

                slot.enabled = true;
                return;
            }
        }

        private void ShowAllPlugs()
        {
            foreach (var slot in _slots)
                slot.enabled = true;
        }
    }
}