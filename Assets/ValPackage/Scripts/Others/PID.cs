using System;
using System.Collections.Generic;
using System.Linq;

namespace ValPackage.Common
{
    /// <summary>
    /// Discrete time PID controller class. The most basic PID controller. Does nothing fancy, just a bare bones implementation.
    /// See <see href="https://github.com/malware-dev/MDK-SE/wiki/PID-Controllers#5-c-implementation">MDK wiki</see>
    /// </summary>
    public class PID
    {
        public float Kp { get; set; } = 0;
        public float Ki { get; set; } = 0;
        public float Kd { get; set; } = 0;
        public float Value { get; private set; }

        float _errorSum = 0;
        float _lastError = 0;
        bool _firstRun = true;

        public PID(float kp, float ki, float kd)
        {
            Kp = kp;
            Ki = ki;
            Kd = kd;
        }

        protected virtual float GetIntegral(float currentError, float errorSum, float deltaTime)
        {
            return errorSum + currentError * deltaTime;
        }

        public float Control(float error, float deltaTime)
        {
            //Compute derivative term
            float errorDerivative = (error - _lastError) * deltaTime;

            if (_firstRun)
            {
                errorDerivative = 0;
                _firstRun = false;
            }

            //Get error sum
            _errorSum = GetIntegral(error, _errorSum, deltaTime);

            //Store this error as last error
            _lastError = error;

            //Construct output
            Value = Kp * error + Ki * _errorSum + Kd * errorDerivative;
            return Value;
        }

        public virtual void Reset()
        {
            _errorSum = 0;
            _lastError = 0;
            _firstRun = true;
        }
    }

    /// <summary>
    /// PID controller where the error integral decays over time so that it does not accumulate too much.
    /// See <see href="https://github.com/malware-dev/MDK-SE/wiki/PID-Controllers#5-c-implementation">MDK wiki</see>
    /// </summary>
    public class DecayingIntegralPID : PID
    {
        public float IntegralDecayRatio { get; set; }

        public DecayingIntegralPID(float kp, float ki, float kd, float decayRatio) : base(kp, ki, kd)
        {
            IntegralDecayRatio = decayRatio;
        }

        protected override float GetIntegral(float currentError, float errorSum, float deltaTime)
        {
            return errorSum * (1 - IntegralDecayRatio) + currentError * deltaTime;
        }
    }

    /// <summary>
    /// PID controller that caps the minimum and maximum error integral value.
    /// See <see href="https://github.com/malware-dev/MDK-SE/wiki/PID-Controllers#5-c-implementation">MDK wiki</see>
    /// </summary>
    public class ClampedIntegralPID : PID
    {
        public float IntegralUpperBound { get; set; }
        public float IntegralLowerBound { get; set; }

        public ClampedIntegralPID(float kp, float ki, float kd, float lowerBound, float upperBound) : base(kp, ki, kd)
        {
            IntegralUpperBound = upperBound;
            IntegralLowerBound = lowerBound;
        }

        protected override float GetIntegral(float currentError, float errorSum, float deltaTime)
        {
            errorSum = errorSum + currentError * deltaTime;
            return MathF.Min(IntegralUpperBound, MathF.Max(errorSum, IntegralLowerBound));
        }
    }

    /// <summary>
    /// PID that uses a fixed length buffer to compute the error integral from.
    /// See <see href="https://github.com/malware-dev/MDK-SE/wiki/PID-Controllers#5-c-implementation">MDK wiki</see>
    /// </summary>
    public class BufferedIntegralPID : PID
    {
        readonly Queue<float> _integralBuffer = new Queue<float>();
        public int IntegralBufferSize { get; set; } = 0;

        public BufferedIntegralPID(float kp, float ki, float kd, int bufferSize) : base(kp, ki, kd)
        {
            IntegralBufferSize = bufferSize;
        }

        protected override float GetIntegral(float currentError, float errorSum, float deltaTime)
        {
            if (_integralBuffer.Count == IntegralBufferSize)
                _integralBuffer.Dequeue();
            _integralBuffer.Enqueue(currentError * deltaTime);
            return _integralBuffer.Sum();
        }

        public override void Reset()
        {
            base.Reset();
            _integralBuffer.Clear();
        }
    }
}