#include "stdafx.h"
#include "Ship.h"

// �h���N���X�̃R���X�g���N�^�ł͖������Ȃ�����.
// �e�N���X�̃f�t�H���g�R���X�g���N�^���Ăяo����邪.
// ���ꂪ�Ȃ��󋵂Ŕ�������G���[.
// https://www.trivia.work/?p=17
CShip::CShip(string name) : CVehicle(name)
{
	Name = name;
	State = STATE::WAIT;
	cout << Name << "�C���X�^���X����" << endl << endl;
}

CShip::~CShip()
{
	cout << Name << "�C���X�^���X�j��" << endl << endl;
}

// http://cpp-lang.sevendays-study.com/day6.html