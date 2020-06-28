using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの動きを制御するクラスです。
public class Player : MonoBehaviour{

	////--------------------------------------------------------------------------------------
	////調整用
	////--------------------------------------------------------------------------------------
	////プレイヤーが傾く速さの上限。
	//const float ANGLE_SPEED_LIMIT = 0.05f;

	////プレイヤーのベースとなるジャンプ力。
	//const float BASE_POWER = 700;

	////プレイヤーがバネの縮みで沈む速さ。
	//const float DESCENT_SPEED = 0.25f;

	////バネのRayを地面に突き刺す量。
	//const float RAY_STICK_VALUE = 0.05f;

	////プレイヤーのバネが縮む速さ。
	//public float _reductionSpeed { get; private set; }

	////バネが縮む上限。
	//public float _springSmallLimit { get; private set; }

	////着地できるまでの間隔。
	//float _groundTimer = 0.2f;
	//const float GROUND_DELAY = 0.2f;
	////--------------------------------------------------------------------------------------

	////プレイヤーに付いているコライダーとRigidBody。
	//Collider2D _col;
	//public Rigidbody2D _rigid { get; private set; }

	////プレイヤーがジャンプする方向。
	//Vector3 _jumpVec;

	////マウス左ボタンを押している時間。
	//float _clickTimer;

	////マウス左ボタンを押している時間分、縮小するバネのオブジェクト。
	//GameObject _playerSpring;

	////バネの元々のスケール（ｘ方向）
	//float _defaultSpringScaleX;
	////バネの元々のスケール（ｙ方向）
	//float _defaultSpringScaleY;

	////普通の地面か氷の地面に着地していたらtrue。
	//public bool _isLandNormalTile { get; private set; }
	//public bool _isLandIceTile { get; private set; }

	////氷の地面で滑る方向。
	//Vector3 _slipDir;

	////クラッシュしたか、ゴールしたかの判定。
	//public bool _isCrash;
	//public bool _isGoal;

	////ゴールしていたらプレイヤーをゴールに吸い込むためのオブジェクト。
	//GameObject _goal;

	////UIManagerのクラスを取得。
	//UIManager _uiManager;
	
	////PauseManagerのクラスを取得。
	//PauseManager _pauseManager;

	//void Awake() {
	//	_rigid = GetComponent<Rigidbody2D>();
	//	_col = GetComponent<Collider2D>();
	//	_playerSpring = GameObject.Find("HopperSpring");
	//	_goal = GameObject.FindGameObjectWithTag("Goal");
	//	_uiManager = FindObjectOfType<UIManager>();
	//	_pauseManager = FindObjectOfType<PauseManager>();
	//}

	//void Start() {
	//	SpawnJudge();

	//	_clickTimer = 0.0f;
	//	_isLandNormalTile = _isLandIceTile = false;
	//	_isCrash = _isGoal = false;
	//	_springSmallLimit = 0.5f;
	//	_reductionSpeed = 0.5f;
	//	_defaultSpringScaleX = _playerSpring.transform.localScale.x;
	//	_defaultSpringScaleY = _playerSpring.transform.localScale.y;
	//}

	//void Update() {
	//	//(ゴールしていない && 衝突していない && ゲーム開始後) は実行されます。
	//	if (!_isCrash && !_isGoal && _uiManager._isGameStart) {

	//		if (Mathf.Approximately(Time.timeScale, 0f) || _pauseManager._isLoadScene)
	//		{
	//			return;
	//		}

	//		PlayerRotation();

	//		_groundTimer -= Time.deltaTime;
	//		if (_groundTimer < 0.0f)
	//		{
	//			GroundJudge();
	//			_groundTimer = 0.0f;
	//		}

	//		FlipHorizontal();

	//		if (_isLandNormalTile) {
	//			if (Input.GetMouseButton(0)) {
	//				JumpEnergyCharge();
	//			}
	//		}
	//		else if (_isLandIceTile) {
	//			float slipSpeed = 2.0f;
	//			transform.position += new Vector3(_slipDir.x * slipSpeed, 0, 0) * Time.deltaTime;
	//			if (Input.GetMouseButton(0)) {
	//				JumpEnergyCharge();
	//			}
	//		}

	//		if (Input.GetMouseButtonUp(0)) {
	//			if (_isLandIceTile || _isLandNormalTile) PlayerJump();
	//		}
	//	}

	//	else if (_isGoal && !_isCrash) {
	//		PlayerInhale();
	//	}
	//}

	////-------------------------------------------------------------------------------------------------------------------------------
	////Start使用関数
	///// <summary>
	///// チェックポイントを通過していたら、チェックポイントからの復帰になります。
	///// </summary>
	//void SpawnJudge() {
	//	int flagMode = PlayerPrefs.GetInt("CHECKPOINT");
	//	if (flagMode == 1)
	//	{
	//		Vector3 checkPoint = GameObject.Find("CheckPoint").transform.position;
	//		transform.position = new Vector3(checkPoint.x, checkPoint.y, 0.0f);
	//		GoalLook();
	//	}
	//	else
	//	{
	//		Vector3 startPoint = GameObject.Find("StartPoint").transform.position;
	//		transform.position = new Vector3(startPoint.x, startPoint.y, 0.0f);
	//	}
	//}

	///// <summary>
	///// プレイヤーは最初ゴールの方向を見ます。
	///// </summary>
	//void GoalLook() {
	//	if (_goal.transform.position.x > transform.position.x)
	//	{
	//		transform.localScale = new Vector3(1, 1, 1);
	//	}
	//	else
	//	{
	//		transform.localScale = new Vector3(-1, 1, 1);
	//	}
	//}

	////-------------------------------------------------------------------------------------------------------------------------------
	////Update使用関数
	///// <summary>
	///// カメラの中心からマウスポインタに、プレイヤーを傾けます。
	///// </summary>
	//void PlayerRotation() {
	//	Vector3 screenMousePos = Input.mousePosition;
	//	screenMousePos.z = -Camera.main.transform.position.z;
	//	Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePos);
	//	Vector3 cameraCenterPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0.0f);
	//	Vector3 targetVec = (mousePosition - cameraCenterPos).normalized;
	//	Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetVec);

	//	//倒しすぎないように判定する関数。
	//	if (LimitRotation(targetRotation)) {
	//		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ANGLE_SPEED_LIMIT);
	//		transform.eulerAngles = new Vector3(0.0f, 0.0f, transform.eulerAngles.z);
	//	}
	//}

	///// <summary>
	///// 地面に倒しすぎないように、頭からRayを左右に出し、動きを止めます。
	///// </summary>
	///// <param name="targetRotation">傾けたい目標の角度</param>
	///// <returns></returns>
	//bool LimitRotation(Quaternion targetRotation) {
	//	//着地中だけ機能する。
	//	if (_isLandIceTile || _isLandNormalTile)
	//	{
	//		float tempRotation;
	//		bool isRotationAvailable = true;

	//		const int RIGHT = 0, LEFT = 1;
	//		var rayOrigin = transform.position + transform.rotation * new Vector3(0.0f, (transform.localScale.y * GetComponent<CapsuleCollider2D>().offset.y), 0.0f);
	//		var rayDir = new Vector3[2];
	//		rayDir[RIGHT] = transform.rotation * Vector3.right;
	//		rayDir[LEFT] = transform.rotation * Vector3.left;

	//		float rayDistance = Mathf.Abs(transform.localScale.x) * GetComponent<CapsuleCollider2D>().size.x / 2 + 0.2f;

	//		var hitRay = new RaycastHit2D[2];
	//		for (int i = 0; i < hitRay.Length; i++)
	//		{
	//			float radias = 0.3f;
	//			hitRay[i] = Physics2D.CircleCast(rayOrigin, radias, rayDir[i], rayDistance);
	//			Debug.DrawLine(rayOrigin, rayOrigin + rayDir[i] * rayDistance, Color.red);
	//			Debug.DrawLine(rayOrigin + rayDir[i] * rayDistance, rayOrigin + rayDir[i] * rayDistance + Vector3.down * radias, Color.red);
	//		}

	//		if (hitRay[LEFT])
	//		{
	//			isRotationAvailable = false;
	//			tempRotation = transform.eulerAngles.z;
	//			if (Mathf.DeltaAngle(tempRotation, targetRotation.eulerAngles.z) < 0)
	//			{
	//				isRotationAvailable = true;
	//			}
	//		}

	//		if (hitRay[RIGHT])
	//		{
	//			isRotationAvailable = false;
	//			tempRotation = transform.eulerAngles.z;
	//			if (Mathf.DeltaAngle(tempRotation, targetRotation.eulerAngles.z) > 0)
	//			{
	//				isRotationAvailable = true;
	//			}
	//		}
	//		return isRotationAvailable;
	//	}
	//	else
	//	{
	//		return true;
	//	}
	//}

	///// <summary>
	///// Rayで地面を判定し、Rayの当たったポジションに着地させる処理です。
	///// </summary>
	//void GroundJudge() {
	//	float fromSpringToWaist = 0.8f;
	//	var rayOrigin = transform.position + transform.rotation * new Vector3(0.0f, fromSpringToWaist, 0.0f);
	//	var rayDirection = transform.rotation * Vector3.down;
	//	float rayDistance = 0.9f;
	//	RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

	//	Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * rayDistance, Color.red);

	//	if (!_isLandNormalTile && !_isLandIceTile && hit2D) {

	//		if (hit2D.collider.tag == "NormalTile" || hit2D.collider.tag == "IceTile") {

	//			_rigid.gravityScale = 0.0f;
	//			_rigid.velocity = Vector2.zero;

	//			var hitPos = new Vector3(hit2D.point.x, hit2D.point.y, 0.0f);
	//			transform.position = hitPos + transform.rotation * new Vector3(0.0f, -RAY_STICK_VALUE, 0.0f);

	//			AudioManager.Instance.PlaySe("Grand");
	//			ParticleManager.Instance.PlayParticle("HitSmoke", gameObject);

	//			if (hit2D.collider.tag == "NormalTile")
	//			{
	//				_isLandNormalTile = true;
	//				_isLandIceTile = false;
	//			}

	//			else if (hit2D.collider.tag == "IceTile")
	//			{
	//				_isLandIceTile = true;
	//				_isLandNormalTile = false;
	//			}
	//		}
	//	}

	//	else if (!hit2D) {
	//		_isLandIceTile = false;
	//		_isLandNormalTile = false;
	//		_rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
	//		_rigid.gravityScale = 1.0f;
	//	}
	//}

	///// <summary>
	///// 左右のスケールを反転する処理です。
	///// </summary>
	//void FlipHorizontal() {
	//	if (transform.eulerAngles.z < 90.0f || transform.eulerAngles.z < 270.0f) {
	//		transform.localScale = new Vector2(-1.0f, 1.0f);
	//	}
	//	else if (transform.eulerAngles.z < 180.0f || transform.eulerAngles.z < 360.0f) {
	//		transform.localScale = new Vector2(1.0f, 1.0f);
	//	}
	//}

	///// <summary>
	///// 着地している間、ジャンプ力を加算します。
	///// また、バネを縮ませプレイヤーを沈めます。
	///// </summary>
	//void JumpEnergyCharge() {
	//	if (_playerSpring.transform.localScale.y > _springSmallLimit) {
	//		_clickTimer += Time.deltaTime;
	//		transform.position += transform.rotation * Vector3.down * (Time.deltaTime * DESCENT_SPEED);
	//		_playerSpring.transform.localScale -= Vector3.up * (Time.deltaTime * _reductionSpeed);
	//	}
	//	else
	//	{
	//		_playerSpring.transform.localScale = new Vector2(_defaultSpringScaleX, _springSmallLimit);
	//	}
	//}

	///// <summary>
	///// プレイヤーがジャンプする処理をしています。同時に氷の時、滑る左右の向きも判定しています。
	///// </summary>
	//void PlayerJump() {
	//	_rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
	//	_rigid.gravityScale = 1.0f;
	//	_jumpVec = (transform.rotation * Vector3.up).normalized;
	//	_rigid.AddForce(_jumpVec * BASE_POWER * _clickTimer, ForceMode2D.Impulse);

	//	AudioManager.Instance.PlaySe("Jump");
	//	ParticleManager.Instance.PlayParticle("JumpSmoke", gameObject);

	//	_playerSpring.transform.localScale = new Vector2(_defaultSpringScaleX, _defaultSpringScaleY);

	//	if (_rigid.velocity.x > 0) _slipDir = Vector3.right.normalized;
	//	else _slipDir = Vector3.left.normalized;

	//	_clickTimer = 0.0f;
	//	_isLandIceTile = false;
	//	_isLandNormalTile = false;
	//	_groundTimer = GROUND_DELAY;
	//}

	///// <summary>
	///// 回転と縮小しながらゴールに引き寄せられる処理をしています。
	///// </summary>
	//void PlayerInhale() {
	//	_col.enabled = false;
	//	_rigid.simulated = false;

	//	float rotateSpeed = 5;
	//	transform.Rotate(0, 0, rotateSpeed);
	//	transform.position += (_goal.transform.position - transform.position) * Time.deltaTime;

	//	//ゴール到着時の左右のスケールで、縮む向きを変えています。
	//	if (transform.localScale.x > 0.0f) {
	//		transform.localScale += new Vector3(-1, -1, -1) * Time.deltaTime;
	//		if (transform.localScale.x < 0.0f || transform.localScale.y < 0.0f) transform.localScale = Vector3.zero;
	//	}
	//	else {
	//		transform.localScale += new Vector3(1, -1, -1) * Time.deltaTime;
	//		if (transform.localScale.x > 0.0f || transform.localScale.y < 0.0f) transform.localScale = Vector3.zero;
	//	}
	//}

	////-----------------------------------------------------------------------------------
	////以下は衝突、トリガー判定処理です。
	////-----------------------------------------------------------------------------------
	///// <summary>
	///// 地面の衝突判定がされたら、勝敗判定が実行されます。
	///// </summary>
	///// <param name="collision">地面のコライダー</param>
	//private void OnCollisionEnter2D(Collision2D collision) {
	//	if (!_isGoal && !_isCrash)
	//	{
	//		if (collision.collider.tag == "NormalTile" || collision.collider.tag == "IceTile")
	//		{
	//			_rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
	//			_rigid.gravityScale = 1.0f;

	//			AudioManager.Instance.PlaySe("ColDamage");

	//			_isCrash = true;
	//			_uiManager.Judgmente();
	//		}
	//	}
	//}

	///// <summary>
	///// トリガー判定されたら、タグでコライダーを見分け、勝敗判定が実行されます。
	///// </summary>
	///// <param name="collision">ゴールか、溶岩か、水中のコライダー</param>
	//private void OnTriggerEnter2D(Collider2D collision) {

	//	if (collision.tag == "Goal")
	//	{
	//		if (!_isGoal && !_isCrash)
	//		{
	//			_isGoal = true;
	//			_uiManager.Judgmente();
	//		}
	//	}

	//	else if (collision.tag == "Lava")
	//	{
	//		_col.enabled = false;
	//		_rigid.simulated = false;

	//		AudioManager.Instance.PlaySe("LavaDamage");
	//		ParticleManager.Instance.PlayParticle("Fire", gameObject);

	//		if (!_isGoal && !_isCrash)
	//		{
	//			_isCrash = true;
	//			_uiManager.Judgmente();
	//		}
	//	}

	//	else if (collision.tag == "Water")
	//	{
	//		_rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
	//		_rigid.gravityScale = 1.0f;

	//		AudioManager.Instance.PlaySe("WaterDive");
	//		ParticleManager.Instance.PlayParticle("WaterSplash", gameObject);

	//		if (!_isGoal && !_isCrash)
	//		{
	//			_isCrash = true;
	//			_uiManager.Judgmente();
	//		}
	//	}
	//}

	///// <summary>
	///// 炎に衝突したら、飛ばされる処理をした後に勝敗判定を実行します。
	///// </summary>
	///// <param name="particle">噴出する炎のパーティクル</param>
	//private void OnParticleCollision(GameObject particle) {
	//	if (particle.tag == "Flame")
	//	{
	//		if (!_isCrash && !_isGoal)
	//		{
	//			_rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
	//			_rigid.gravityScale = 1.0f;

	//			Vector3 damageVec = (transform.position - particle.transform.position).normalized;
	//			_rigid.AddForce(damageVec * BASE_POWER, ForceMode2D.Impulse);

	//			AudioManager.Instance.PlaySe("FireDamage");
	//			ParticleManager.Instance.PlayParticle("FireExplosion", gameObject);

	//			_isCrash = true;
	//			_uiManager.Judgmente();
	//		}
	//	}
	//}
}